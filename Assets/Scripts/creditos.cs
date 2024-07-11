using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class creditos : MonoBehaviour
{
    public void Sair()
    {
        Application.Quit();
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
