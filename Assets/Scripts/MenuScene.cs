using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("intro");
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
