using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public float vidasinimigo = 0;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            if ( vidasinimigo != 3)
            {
                vidasinimigo ++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
