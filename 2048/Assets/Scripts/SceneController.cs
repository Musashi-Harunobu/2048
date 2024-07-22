using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        GameManager.IsGameOver = false;
    }
    public static void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
