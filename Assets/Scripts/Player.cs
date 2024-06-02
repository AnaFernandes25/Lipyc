using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody rb;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed, jumpForce, playerHeight;
    public bool walking;
    public Transform playerTrans;
    private bool isGrounded;
    public LayerMask whatIsGround;
    bool readyToJump = true;

    public string nivelACarregarLost;
    public string nivelACarregarWin; // Adicionado para carregar nível quando jogador ganhar
    public Transform respawnPoint;
    private int vidasIniciais = 4;
    public List<Image> vidas;

    private int rewardsCollected = 0; // Contador de recompensas
    public int totalRewards = 5; // Número total de recompensas necessárias para ganhar

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetJump();
    }

    void FixedUpdate()
    {
        // Movimentação no eixo Z (frente e trás)
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward * w_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -transform.forward * wb_speed * Time.deltaTime;
        }
        movement.y = rb.velocity.y; // Manter a velocidade Y existente para não interferir no pulo
        rb.velocity = movement;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            readyToJump = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HandleDamage();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        rb.freezeRotation = true;

        // Animação e estado de caminhada
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
        }

        // Rotação do jogador
        if (Input.GetKey(KeyCode.A))
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
        }

        // Corrida
        if (walking)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                w_speed += rn_speed;
                playerAnim.SetTrigger("dash");
                playerAnim.ResetTrigger("walk");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed;
                playerAnim.ResetTrigger("dash");
                playerAnim.SetTrigger("walk");
            }
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && readyToJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Zera apenas a velocidade Y
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("jump");
            isGrounded = false;
            readyToJump = false;
        }

        // Ataque
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            playerAnim.SetTrigger("attack1");
        }
        if (Input.GetMouseButtonDown(1)) // Botão direito do mouse
        {
            playerAnim.SetTrigger("attack2");
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
            SceneManager.LoadScene(nivelACarregarLost);
        }
        else
        {
            transform.position = respawnPoint.position;
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LimiteEcra")
        {
            HandleDamage();
        }
        else if (other.tag == "Checkpoint")
        {
            respawnPoint.position = transform.position;
        }
    }

    public void CollectReward(GameObject reward)
    {
        rewardsCollected++;
        Destroy(reward); // Destruir a recompensa coletada

        // Verifica se o jogador coletou todas as recompensas
        if (rewardsCollected >= totalRewards)
        {
            SceneManager.LoadScene(nivelACarregarWin);
        }
    }
}
