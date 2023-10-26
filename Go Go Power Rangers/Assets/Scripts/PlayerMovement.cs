using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        NEUTRAL,
        RED,
        GREEN,
        BLUE
    }
    public PlayerState playerState;

    private Renderer playerRenderer;

    [Tooltip("[1, Infinity]")]
    [SerializeField] private float forwardSpeed = 30f;
    private Vector3 direction;
    private int lane = 0;
    [SerializeField] private float laneWidth = 3f;

    private float nextPlaneZ = 50;
    private float planeLength = 100;
    private bool nearToNextPlane = false;
    private float markToDestroy = 70;
    private bool flagToDestroy = false;
    public bool greenPowerup = false;

    private int[,] oldIntArray = new int[10, 3];
    public GameObject[,] oldGameObjectArray = new GameObject[10, 3];
    private int[,] newIntArray = new int[10, 3];
    public GameObject[,] newGameObjectArray = new GameObject[10, 3];

    [SerializeField] private GameObject planePrefab;
    [SerializeField] private GameObject redOrbPrefab;
    [SerializeField] private GameObject greenOrbPrefab;
    [SerializeField] private GameObject blueOrbPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    public AudioSource invalidInputAudio;

    private Queue<GameObject> planes = new Queue<GameObject>();

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.NEUTRAL; 
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        GameObject plane = Instantiate(planePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        planes.Enqueue(plane);
        generateStart();
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;
        Vector3 postitionToGo = transform.position;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (lane < 1)
            {
                lane++;
                postitionToGo.x += laneWidth;
                transform.position = postitionToGo;
            }
            else 
                invalidInputAudio.Play();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (lane > -1)
            {
                lane--;
                postitionToGo.x -= laneWidth;
                transform.position = postitionToGo;
            }
            else
                invalidInputAudio.Play();
        }

        
        nearToNextPlane = checkIfNearToNextPlane(transform.position.z);

        if (nearToNextPlane)
        {
            generateNewPlane();
            generateIntArray(newIntArray);
            generateGameObjectArray(newIntArray, newGameObjectArray);
        }

        flagToDestroy = timeToDestroy(transform.position.z);

        if (flagToDestroy)
        {
            destroyLastPlane();
        }

        checkStateAndAlter();
    }

    private void FixedUpdate()
    {
        rb.velocity = forwardSpeed * direction * Time.deltaTime;
    }

    bool checkIfNearToNextPlane(float z)
    {
        
        if (nextPlaneZ - z < 50)
            return true;
        return false;
    }

    void generateNewPlane()
    {
        if (planePrefab != null)
        {
            GameObject newPlane = Instantiate(planePrefab, new Vector3(0, 0, nextPlaneZ + 50), Quaternion.identity);
            planes.Enqueue(newPlane);
        }
        nextPlaneZ += planeLength;
    }

    void generateIntArray(int[,] arr)
    {
        int numberOfObstacles = 0;
        int randomLane = 0;
        int randomObject = 0;
        bool flag = true;
        for(int i = 0; i < 10; i++)
        {
            numberOfObstacles = Random.Range(0, 3);
            for(int j = 0; j < numberOfObstacles; j++)
            {
                flag = true;
                while (flag)
                {
                    randomLane = Random.Range(0, 3);
                    if (arr[i, randomLane] == 0) {
                        arr[i, randomLane] = 1;
                        flag = false;
                    }
                }
            }
            for(int j = 0; j < 3; j++)
            {
                if (arr[i, j] == 0)
                {
                    randomObject = Random.Range(2, 10);
                    arr[i, j] = randomObject;
                }
            }
        }
    }

    void generateGameObjectArray(int[,] arr, GameObject[,] gameObjectArray)
    {
        int currentLaneObject = 0;
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                currentLaneObject = arr[i, j];
                switch(currentLaneObject)
                {
                    case 4:
                        gameObjectArray[i, j] = Instantiate(redOrbPrefab, new Vector3(-3 + (3 * j), 0, nextPlaneZ - 95 + (10 * i)), Quaternion.identity);
                        break;
                    case 2:
                        gameObjectArray[i, j] = Instantiate(greenOrbPrefab, new Vector3(-3 + (3 * j), 0, nextPlaneZ - 95 + (10 * i)), Quaternion.identity);
                        break;
                    case 3:
                        gameObjectArray[i, j] = Instantiate(blueOrbPrefab, new Vector3(-3 + (3 * j), 0, nextPlaneZ - 95 + (10 * i)), Quaternion.identity);
                        break;
                    case 1:
                        gameObjectArray[i, j] = Instantiate(obstaclePrefab, new Vector3(-3 + (3 * j), 0, nextPlaneZ - 95 + (10 * i)), Quaternion.identity);
                        break;
                }
            }
        }
    }

    bool timeToDestroy(float z)
    {
        if(markToDestroy < z)
        {
            markToDestroy += planeLength;
            return true;
        }
        return false;
    }
 
    void destroyLastPlane()
    {
        GameObject plane = planes.Peek();
        Destroy(plane);
        planes.Dequeue();
        destroyOldObjects();
        copyContentOfNewArraysToOldArrays();
        flagToDestroy = false;
    }

    void destroyOldObjects()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Destroy(oldGameObjectArray[i, j]);
            }
        }
    }

    void copyContentOfNewArraysToOldArrays()
    {
        for (int i = 0; i < 10; i++) {
            for(int j = 0; j < 3; j++)
            {
                oldIntArray[i, j] = newIntArray[i, j];
                oldGameObjectArray[i, j] = newGameObjectArray[i, j];
                newIntArray[i, j] = 0;
            }
        }
    }

    void generateStart()
    {
        generateIntArray(oldIntArray);
        generateGameObjectArray(oldIntArray, oldGameObjectArray);
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (oldGameObjectArray[i,j] != null)
                    Destroy(oldGameObjectArray[i,j]);
            }
        }
    }

    void checkStateAndAlter()
    {
        Color playerColor = Color.white;
        switch (playerState)
        {
            case PlayerState.RED:
                playerColor = Color.red;
                break;
            case PlayerState.GREEN:
                playerColor = Color.green;
                break;
            case PlayerState.BLUE:
                playerColor = Color.blue;
                break;
        }
        playerRenderer.material.color = playerColor;
    }

}
