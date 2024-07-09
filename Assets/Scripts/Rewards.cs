using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Adicione esta linha para usar o SceneManager

public class Rewards : MonoBehaviour
{
    public int pointToAdd;

    void Start()
    {
        // Registra para ouvir eventos de mudança de cena
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<Player>() == null)
            return;

        ScoreManager.AddPoints(pointToAdd);

        // Desativa o componente MeshRenderer
        GetComponentInChildren<MeshRenderer>().enabled = false;

        // Destrói o objeto após 1 segundo
        Destroy(gameObject, 1.0f);
    }

    void OnSceneUnloaded(Scene scene)
    {
        ScoreManager.Reset();
    }

    void OnDestroy()
    {
        // Certifique-se de cancelar a inscrição dos eventos para evitar possíveis problemas
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
