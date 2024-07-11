using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject winPanel; // Refer�ncia ao painel de vit�ria
    public Button menuButton; // Refer�ncia ao bot�o de menu

    void Start()
    {

        winPanel.SetActive(false);

        menuButton.onClick.AddListener(LoadMenu);
    }

    void Update()
    {
        if (PlayerHasWon())
        {
            ShowWinPanel();
        }
    }

    void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    bool PlayerHasWon()
    {
        // Sua l�gica de vit�ria aqui
        return false; // Exemplo: substitua com sua l�gica
    }
}

