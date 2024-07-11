using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScene : MonoBehaviour
{
    public Button res;
    public Button menu;
    public Button sair;
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Control()
    {
        SceneManager.LoadScene("Controlos");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

}
