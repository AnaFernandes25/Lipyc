using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject winPanel; // Referência ao painel de vitória
    public Button menuButton; // Referência ao botão de menu

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
        // Sua lógica de vitória aqui
        return false; // Exemplo: substitua com sua lógica
    }
}

