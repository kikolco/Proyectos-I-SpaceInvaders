using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject endMenuScreen;
    public void GameOver()
    {
        endMenuScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
