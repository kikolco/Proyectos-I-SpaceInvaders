using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Inicio de variables
    public static ScoreManager instance;

    public Text scoreText;
    public Text multiplierText;
    public Text highscoreText;

    public int score = 0;
    int highscore = 0;
    int multiplier = 1;

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
        score += (1 * multiplier);
        scoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }

    public void AddMultiplier() 
    {
        multiplier += 1;
        multiplierText.text = "X " + multiplier.ToString();
        //gameObject.GetComponent<Animation>().Play("multiplier_add");
    }

    public void RemoveMultiplier() 
    {
        multiplier = 1;
        multiplierText.text = "";
    }
}
