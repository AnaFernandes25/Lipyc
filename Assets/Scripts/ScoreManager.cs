using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static TMP_Text textoPontuacao;
    private static int score;

    void Start()
    {
        textoPontuacao = GetComponent<TMP_Text>();
        score = PlayerPrefs.GetInt("score");

        if (score < 0)
        {
            score = 0;
        }

        textoPontuacao.text = score.ToString(); // Melhorando a conversão para string
    }

    public static void AddPoints(int points)
    {
        score += points;
        PlayerPrefs.SetInt("score", score);
        textoPontuacao.text = score.ToString(); // Melhorando a conversão para string
    }

    public static int GetPoints()
    {
        return score;
    }

    public static void Reset()
    {
        score = 0;
        PlayerPrefs.SetInt("score", score);
        if (textoPontuacao != null)
        {
            textoPontuacao.text = score.ToString(); // Melhorando a conversão para string
        }
    }
}
