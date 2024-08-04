using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Vector3 Direction { get; private set; }
    
    [SerializeField] private float speed = 30f;
    [SerializeField] private float pushPower = 30f;
    [SerializeField] private float coolDown = 1f;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private GameObject cubePrefab;

    private GameObject cube;
    private float timeToCreation = 0f;
    
    private Camera mainCamera;
    

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    private void Push()
    {
        
        Rigidbody cubeRB = cube.GetComponent<Rigidbody>();
        cubeRB.isKinematic = false;
        cubeRB.AddForce(Vector3.right * pushPower, ForceMode.VelocityChange);
        cube = null;
        
    }

    private void CreateCube()
    {
        cubePrefab.GetComponent<CubeController>().CubeNumber = GetRandomCubeNumber();
        
        cube = Instantiate(cubePrefab);
        
        cube.tag = "NewCube";

        Vector3 center = (pointA.position + pointB.position) / 2f;

        cube.transform.position = center;
    }

    private int GetRandomCubeNumber()
    {
        int number = 2;
        int iterations = 0;

        for (int i = 2; i <= GameManager.HighestNumberOnCube; i += i)
        {
            iterations++;
        }

        int rnd = Random.Range(0, iterations);

        for (int i = 0; i < rnd; i++)
        {
            number += number;
            if (number > 8)
            {
                number = 2;
            }
            
        }

        return number;
    }

    private void Movement()
    {
        Vector3 newPosititon = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosititon.x = 0f;
        newPosititon.y = 0f;

        Direction = newPosititon - transform.position;
        float velocity = Direction.magnitude / Time.deltaTime;
        cube.transform.position = Vector3.Lerp(pointA.position, pointB.position, velocity);
        transform.position = newPosititon;
    }

    private void FixedUpdate()
    {
        if (timeToCreation < Time.time && GameManager.IsGameOver != true)
        {
            if (Input.GetMouseButton(0) && cube == null)
            {
                // Создать новый куб
                CreateCube();
            }

            if (cube != null)
            {
                // Смещение объекта относительно положения мыши на экране
                if (Input.GetMouseButton(0))
                {
                    // float screenWidth = Screen.width;
                    // float mouseX = Input.mousePosition.x / screenWidth;
                    // cube.transform.position = Vector3.Lerp(pointA.position, pointB.position, mouseX);
                    Vector3 screenPointA = Camera.main.WorldToScreenPoint(pointA.position);
                    Vector3 screenPointB = Camera.main.WorldToScreenPoint(pointB.position);
                    float limitedMouseX = Mathf.Clamp(Input.mousePosition.x, screenPointA.x, screenPointB.x);
                    float lerpFactor = (limitedMouseX - screenPointA.x) / (screenPointB.x - screenPointA.x);
                    cube.transform.position = Vector3.Lerp(pointA.position, pointB.position, lerpFactor); }
                else if (!Input.GetMouseButton(0))
                {
                    // Толкнуть куб вперед
                    Push();
                    timeToCreation = Time.time + coolDown;
                }
            }
        }
    }
}
