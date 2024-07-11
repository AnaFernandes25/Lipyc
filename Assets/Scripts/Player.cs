using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float dashSpeedMultiplier = 2f;
    public float dashDuration = 0.5f;

    [Header("Animation")]
    public Animator playerAnim;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool readyToJump = true;

    [Header("Camera and Orientation")]
    public Transform orientation;
    public Camera mainCamera;

    [Header("Health and Respawn")]
    public int vidasIniciais = 3;
    public List<Image> vidas;

    [Header("Level Loading")]
    public string nivelACarregarLost;
    public string nivelACarregarWin;
    private int rewardsCollected = 0;
    public int totalRewards = 5;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private float originalSpeed;
    private Coroutine waterDamageCoroutine;
    private Vector3 checkpointPosition;
    public Transform player;
    private Ultimocheck ultimocheck;
    public GameObject gameOverScreen;
    private MeshRenderer meshRenderer;
    private Collider playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalSpeed = moveSpeed;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        ResetJump();
        //LoadCheckpoint();
        Cursor.visible = false;
    }

    void Update()
    {
        
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.8f + 0.6f, whatIsGround);

        MyInput();
        SpeedControl();

        // Handle drag
        rb.drag = grounded ? groundDrag : 0;

        // Adjust player rotation to face the camera's direction
        Vector3 directionToCamera = (mainCamera.transform.position - transform.position).normalized;
        directionToCamera.y = 0;
        transform.rotation = Quaternion.LookRotation(-directionToCamera);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            playerAnim.SetTrigger("jump");
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Dash
        if (grounded && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            StartCoroutine(Dash());
        }

        // Handle animations
        HandleAnimations();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Get the forward and right directions relative to the camera
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        // Project the forward and right directions onto the horizontal plane (y = 0)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calculate the movement direction based on input and camera orientation
        moveDirection = forward * verticalInput + right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private IEnumerator Dash()
    {
        moveSpeed *= dashSpeedMultiplier;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed = originalSpeed;
    }

    private void HandleAnimations()
    {
        bool walking = horizontalInput != 0 || verticalInput != 0;

        if (walking)
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
        }
        else
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            readyToJump = true;
        }
        if (collision.gameObject.CompareTag("inimigo"))
        {
            HandleDamage();
        }
        /*if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpointPosition = collision.transform.position;
            //SaveCheckpoint();
        }*/
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("LimiteEcra"))
        //{
        //    HandleDamage();
        //}
        
        if (other.CompareTag("Checkpoint"))
        {
            ultimocheck.ultimo = other.transform.position;
            
        }
        else if (other.CompareTag("agua"))
        {
            HandleDamage();
            if (waterDamageCoroutine == null)
            {
                waterDamageCoroutine = StartCoroutine(WaterDamage());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("agua") && waterDamageCoroutine != null)
        {
            StopCoroutine(waterDamageCoroutine);
            waterDamageCoroutine = null;
        }
    }

    private IEnumerator WaterDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            HandleDamage();
        }
    }

    private void HandleDamage()
    {
        if (vidasIniciais > 0)
        {
            vidas[vidasIniciais - 1].gameObject.SetActive(false);
            vidasIniciais--;
        }

        if (vidasIniciais == 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        // Desativar a mesh e o movimento do jogador
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // Exibir a tela de game over
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        Cursor.visible = true;
        ScoreManager.Reset();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*private void SaveCheckpoint()
    {
        if(col.gameObject.CompareTag("Player"))
        Player.transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();//Isto faz update dos transforms
    }

    private void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            checkpointPosition = new Vector3(x, y, z);
        }
        else
        {
            checkpointPosition = respawnPoint.position;
        }
    }*/
}
