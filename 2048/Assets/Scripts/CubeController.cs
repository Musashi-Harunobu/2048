using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    public int CubeNumber = 2;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private TextMeshProUGUI[] cubeInfo;
    [SerializeField] private Color[] cubeColors;
    [SerializeField] private Material[] cubeMaterials;



    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        UpdateInformation();
        
        AssignCubeColor();
    }
    
    private void AssignCubeColor()
    {
        // Color cubeColor = Color.white;
        Material cubeMaterial = null;
        int colorID = 0;

        for(int i = 2; i <= CubeNumber; i += i)
        {
            if (colorID < cubeMaterials.Length)
            {
                cubeMaterial = cubeMaterials[colorID];
            }
        
            colorID++;
        }
        GetComponent<Renderer>().material = cubeMaterial;
    }


    private void UpdateInformation()
    {
        for (int i = 0; i < cubeInfo.Length; i++)
        {
            cubeInfo[i].text = $"{CubeNumber}";
        }
        if(CubeNumber > GameManager.HighestNumberOnCube)
        {
            GameManager.HighestNumberOnCube = CubeNumber;
        }
        AssignCubeColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем что это тоже кубик
        if (collision.gameObject.GetComponent<CubeController>())
        {
            // Сравниваем значения на кубиках
            if (collision.gameObject.GetComponent<CubeController>().CubeNumber == CubeNumber)
            {
                // Рассчитываем центральную точку между кубами
                Vector3 thisCubePos = transform.position;
                Vector3 collisionCubePos = collision.gameObject.transform.position;
                Vector3 center = (thisCubePos + collisionCubePos) / 2f;
                
                Destroy(collision.gameObject);

                transform.position = center;

                rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

                CubeNumber += CubeNumber;
                
                GameManager.UpdateScore(CubeNumber);
                
                if(CubeNumber > GameManager.HighestNumberOnCube)
                {
                    GameManager.HighestNumberOnCube = CubeNumber;
                }
                
                UpdateInformation();
            }
        }
    }
}
