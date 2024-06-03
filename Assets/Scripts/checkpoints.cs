using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoints : MonoBehaviour
{
    

    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Vector3 novoCheckPoint;
    [SerializeField] float dead;

    void Update()
    {
        if(player.transform.position.y > -dead)
        {
            player.transform.position = novoCheckPoint;
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        novoCheckPoint = player.transform.position;
        Destroy(col.gameObject);
    }
}
