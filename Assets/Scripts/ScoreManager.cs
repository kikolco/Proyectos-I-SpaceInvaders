using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Inicio de variables
    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake() {
        instance = this;
    }

    // Resetea todos los textos a 0 puntos
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0); //coje el highscore de la última partida
        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }

    // Función pública para añadir puntos y guardar los highscore
    public void AddPoint() 
    {
        score += 1;
        scoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }
}
