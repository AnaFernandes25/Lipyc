using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechTxt;
    public string actorName;

    public LayerMask playerLayer;
    public float radius;

    private DialogueControl dc;
    private bool onRadius;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onRadius)
        {
            dc.Speech(profile, speechTxt, actorName);
        }

        if (Input.GetKeyDown(KeyCode.Return) && dc.dialogueObj.activeSelf)
        {
            dc.NextSentence();
        }
    }

    private void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
        {
            onRadius = true;
        }
        else
        {
            onRadius = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
