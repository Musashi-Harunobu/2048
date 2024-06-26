using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float pushPower = 30f;
    [SerializeField] private float CoolDown = 1f;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private GameObject cubePrefab;

    private GameObject cube;
    private float timeToCreation = 0f;

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
        }

        return number;
    }


    private void FixedUpdate()
    {
        if (timeToCreation < Time.time && GameManager.IsGameOver != true)
        {
            if (Input.touchCount > 0 && cube == null)
            {
                // Создать новый куб
                CreateCube();
            }

            if (cube != null)
            {
                // Смещение объекта относительно положения пальца на экране
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    float screenWidth = Screen.width;

                    float touchPosX = Input.GetTouch(0).position.x / screenWidth;

                    cube.transform.position = Vector3.Lerp(pointA.position, pointB.position, touchPosX);

                }
                else if (Input.touchCount == 0)
                {
                    // Толкнуть куб вперед
                    Push();

                    timeToCreation = Time.time + CoolDown;
                }
            }
        }
    }
}    
