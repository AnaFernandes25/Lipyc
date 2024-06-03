using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static TMP_Text textoPontuacao;
    private static int score;

    // Start is called before the first frame update
    void Start()
    {
        textoPontuacao = GetComponent<TMP_Text>();
        score = PlayerPrefs.GetInt("score");

        if (score < 0)
        {
            score = 0;
        }

        textoPontuacao.text = "" + score; //Falta melhorar o estilo
    }
    public static void AddPoints(int points)
    {
        score += points;

        PlayerPrefs.SetInt("score", score);

        textoPontuacao.text = "" + score; // falta melhorar o estilo
    }

    public static int GetPoints()
    {
        return score;
    }

    public static void Reset()
    {
        score = 0;

        PlayerPrefs.SetInt("score", score);

        textoPontuacao.text = "" + score; // falta melhorar o estilo
    }

    // Update is called once per frame
    void Update()
    {

    }
}