using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target; // O jogador que a câmera vai seguir
    public Vector3 offset; // A posição de deslocamento da câmera em relação ao jogador
    public float smoothSpeed = 0.125f; // A velocidade de suavização da câmera

    void LateUpdate()
    {
        // Calcula a posição desejada da câmera com base na posição e rotação do jogador
        Vector3 desiredPosition = target.position + target.rotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suavização da transição
        transform.position = smoothedPosition; // Atualiza a posição da câmera

        transform.LookAt(target); // Faz a câmera olhar para o jogador
    }
}
