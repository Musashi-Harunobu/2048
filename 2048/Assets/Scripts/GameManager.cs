using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int CurrentGameScore;
    public static int RecordGameScore;
    public static int HighestNumberOnCube;

    public static bool IsGameOver;
    
    private void Awake()
    {
        // Обнулить текущий результат

        IsGameOver = false;

        CurrentGameScore = 0;

        HighestNumberOnCube = 0;
        // Найти все кубики на сцене
        CubeController[] cubes = FindObjectsOfType<CubeController>();

        // Поиск наибольшего значения среди кубиков
        for (int i = 0; i < cubes.Length; i++)
        {
            if(cubes[i].CubeNumber > HighestNumberOnCube)
            {
                HighestNumberOnCube = cubes[i].CubeNumber;
            }
        }
    }

    public static void UpdateScore(int score)
    {
        CurrentGameScore += score;

        if (RecordGameScore < CurrentGameScore)
        {
            CurrentGameScore = RecordGameScore;
        }
    }
}
