using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    public static int Countdown = 0;

    [SerializeField] private int countdownToLoss = 3;

    private GameObject newCube;

    IEnumerator GameOverTime()
    {
        Countdown = countdownToLoss;

        for(int i = 0; i < Countdown; Countdown--)
        {
            // Остановить работу на 1 секунду
            yield return new WaitForSeconds(1f);
        }

        GameManager.IsGameOver = true;

        // Остановить все корутины
        StopAllCoroutines();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NewCube"))
        {
            if (newCube != null)
            {
                if (Countdown == 0)
                {
                    StartCoroutine(GameOverTime());
                }
            }
            else
            {
                newCube = other.gameObject;
            }
        }
        if (other.CompareTag("SimpleCube"))
        {
            if (Countdown == 0)
            {
                StartCoroutine(GameOverTime());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NewCube"))
        {
            other.tag = "SimpleCube";
            newCube = null;
        }
        else if (other.CompareTag("SimpleCube"))
        {
            StopAllCoroutines();
            Countdown = 0;
        }
    }

}
