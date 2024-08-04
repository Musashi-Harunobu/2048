using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;


public class SceneController : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        GameManager.IsGameOver = false;
        YandexGame.FullscreenShow();
    }
    public static void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
