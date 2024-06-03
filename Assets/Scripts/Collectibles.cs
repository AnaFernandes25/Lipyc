using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    [SerializeField] Text texto;
    [SerializeField] GameObject player;
    [SerializeField] bool isCheckPoint = false;
    float quantidadeluz = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            quantidadeluz += 1;
            if (isCheckPoint == true)
            {
                player.transform.position = gameObject.transform.position;
            }
            texto.text = "Luzes:" + quantidadeluz;
            Destroy(gameObject);
        }
    }
}
