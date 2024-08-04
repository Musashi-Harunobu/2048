using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class RestrictedArea : MonoBehaviour
{
    public static int Countdown = 0;

    [SerializeField] private int countdownToLoss = 1;

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
    
    IEnumerator DestroyAfterDelay(GameObject cube)
    {
        yield return new WaitForSeconds(0.5f); // Задержка перед уничтожением
        Destroy(cube);
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
                StartCoroutine(DestroyAfterDelay(other.gameObject)); // Запуск корутины с задержкой
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
