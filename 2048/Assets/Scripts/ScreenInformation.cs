using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentGameScoreInfo;
    [SerializeField] private TextMeshProUGUI RecordGameScoreInfo;
    [SerializeField] private TextMeshProUGUI HighestNumberOnCubeInfo;

    private void FixedUpdate()
    {
        CurrentGameScoreInfo.text = $"{GameManager.CurrentGameScore}";
        RecordGameScoreInfo.text = $"{GameManager.RecordGameScore}";
        HighestNumberOnCubeInfo.text = $"{GameManager.HighestNumberOnCube}";
    }

}
