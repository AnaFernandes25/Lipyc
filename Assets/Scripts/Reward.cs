using UnityEngine;

public class RewardPickup : MonoBehaviour
{
    public int pointToAdd = 1; // Pontos a serem adicionados ao coletar a recompensa
    private AudioSource rewardPickupEffect;

    void Start()
    {
        rewardPickupEffect = GetComponent<AudioSource>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<player>() == null) // Verifica se o jogador colidiu com a recompensa
            return;

        // Adiciona pontos ou chama o método para coletar a recompensa
        other.GetComponent<player>().CollectReward(gameObject);

        rewardPickupEffect.Play(); // Toca o som de coleta

        GetComponentInChildren<MeshRenderer>().enabled = false; // Desativa o componente MeshRenderer

        Destroy(gameObject, 1.0f); // Destrói o objeto após 1 segundo
    }
}
