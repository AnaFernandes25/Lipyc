using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{

    public int pointToAdd;

    //private AudioSource RewardPickupEffect;



    void Start()
    {
        //RewardPickupEffect = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<Player>() == null)
            return;

        ScoreManager.AddPoints(pointToAdd);

        //RewardPickupEffect.Play(); // Toca o som

        GetComponentInChildren<MeshRenderer>().enabled = false; // Desativa o componente MeshRenderer

        Destroy(gameObject, 1.0f); // Destrói o objeto após 1 segundo
    }
}
