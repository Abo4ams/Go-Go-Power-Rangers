using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public PlayerMovement movementScript;
    public PlayerCollision collisionScript;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        collisionScript = GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (collisionScript.red == 5)
            {
                movementScript.playerState = PlayerMovement.PlayerState.RED;
                collisionScript.red--;
                collisionScript.redText.text = "Points: " + collisionScript.red;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (collisionScript.green == 5)
            {
                movementScript.playerState = PlayerMovement.PlayerState.GREEN;
                collisionScript.green--;
                collisionScript.greenText.text = "Points: " + collisionScript.green;
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (collisionScript.blue == 5)
            {
                movementScript.playerState = PlayerMovement.PlayerState.BLUE;
                collisionScript.blue--;
                collisionScript.blueText.text = "Points: " + collisionScript.blue;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            performSuperPower();
        }
        checkIfPointsAreZero();
    }

    void performSuperPower()
    {
        switch (movementScript.playerState)
        {
            case PlayerMovement.PlayerState.RED:
                collisionScript.red--;
                collisionScript.redText.text = "Points: " + collisionScript.red;
                destroyAllObstacles();
                break;
            case PlayerMovement.PlayerState.GREEN:
                if(collisionScript.green > 1)
                {
                    if (!movementScript.greenPowerup)
                    {
                        movementScript.greenPowerup = true;
                        collisionScript.green--;
                        collisionScript.greenText.text = "Points: " + collisionScript.green;
                    }
                }
                else
                {
                    collisionScript.green--;
                    collisionScript.greenText.text = "Points: " + collisionScript.green;
                }
                break;
            case PlayerMovement.PlayerState.BLUE:
                if (collisionScript.blue > 1)
                {
                    if (collisionScript.blueShield == false)
                    {
                        collisionScript.blue--;
                        collisionScript.blueText.text = "Points: " + collisionScript.blue;
                    }
                    collisionScript.blueShield = true;
                }
                else
                {
                    collisionScript.blue--;
                    collisionScript.blueText.text = "Points: " + collisionScript.blue;
                }
                break;
        }
    }

    void destroyAllObstacles()
    {
        GameObject[,] oldGameObjectArray = movementScript.oldGameObjectArray;
        GameObject[,] newGameObjectArray = movementScript.newGameObjectArray;
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (oldGameObjectArray[i,j] != null && oldGameObjectArray[i,j].tag.Equals("obstacle"))
                    Destroy(oldGameObjectArray[i,j]);
                if (newGameObjectArray[i,j] != null && newGameObjectArray[i,j].tag.Equals("obstacle"))
                    Destroy(newGameObjectArray[i,j]);
            }
        }
    }

    void checkIfPointsAreZero()
    {
        switch (movementScript.playerState)
        {
            case PlayerMovement.PlayerState.RED:
                if(collisionScript.red == 0)
                    movementScript.playerState = PlayerMovement.PlayerState.NEUTRAL;
                break;
            case PlayerMovement.PlayerState.GREEN:
                if (collisionScript.green == 0)
                    movementScript.playerState = PlayerMovement.PlayerState.NEUTRAL;
                break;
            case PlayerMovement.PlayerState.BLUE:
                if (collisionScript.blue == 0)
                    movementScript.playerState = PlayerMovement.PlayerState.NEUTRAL;
                break;
        }
    }

}
