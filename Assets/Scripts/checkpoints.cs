using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class checkpoints : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<Ultimocheck>().ultimo = GetComponent<Transform>().position;
        }
    }
}
