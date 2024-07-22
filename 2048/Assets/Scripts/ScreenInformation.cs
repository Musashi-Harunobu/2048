using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentGameScoreInfo;
    [SerializeField] private TextMeshProUGUI RecordGameScoreInfo;

    
    [SerializeField] private GameObject GameOverWindow;

    private void FixedUpdate()
    {
        CurrentGameScoreInfo.text = $"Счет: {GameManager.CurrentGameScore}";
        RecordGameScoreInfo.text = $"Лучший счет: {GameManager.RecordGameScore}";
        
        if (GameManager.IsGameOver == true &&  GameOverWindow.activeSelf == false)
        {
            GameOverWindow.SetActive(true);
        }
    }

}
