using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public Transform playerTransform; // Assumindo que você tem uma referência ao Transform do jogador

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Alinha o bulletSpawnPoint com a rotação do jogador
        bulletSpawnPoint.rotation = playerTransform.rotation;

        // Instancia a bala no ponto de spawn
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Define a velocidade da bala na direção do ponto de spawn
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
}
