using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private static int score = 0;
    public int red = 0;
    public int green = 0;
    public int blue = 0;
    public bool blueShield = false;
    private int collisionTimeout = 10;
    [SerializeField] public TMP_Text redText;
    [SerializeField] public TMP_Text greenText;
    [SerializeField] public TMP_Text blueText;
    [SerializeField] private TMP_Text scoreText;

    public PlayerMovement movementScript;

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        collisionTimeout = Mathf.Min(10, collisionTimeout + 1);
        scoreText.text = "Score: " + score;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        GameObject parentPrefab = null;
        int pointsToAdd = 1;
        int scoreToAdd = 1;

        if (collidedObject.tag.Equals("Untagged"))
        {
            parentPrefab = collidedObject.transform.parent.gameObject;
        }

        if (movementScript.greenPowerup)
        {
            pointsToAdd = 2;
            scoreToAdd = 5;
            movementScript.greenPowerup = false;
        }

        if (parentPrefab != null && parentPrefab.tag.Equals("redOrb"))
        {
            if(movementScript.playerState == PlayerMovement.PlayerState.RED)
            {
                scoreToAdd *= 2;
            }
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
            red = Mathf.Min(5, red + pointsToAdd);
            redText.text = "Points: " + red;
            Destroy(parentPrefab);
        }
        if (parentPrefab != null && parentPrefab.tag.Equals("greenOrb"))
        {
            if (movementScript.playerState == PlayerMovement.PlayerState.GREEN)
            {
                scoreToAdd *= 2;
            }
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
            green = Mathf.Min(5, green + pointsToAdd);
            greenText.text = "Points: " + green;
            Destroy(parentPrefab);
        }
        if (parentPrefab != null && parentPrefab.tag.Equals("blueOrb"))
        {
            if (movementScript.playerState == PlayerMovement.PlayerState.BLUE)
            {
                scoreToAdd *= 2;
            }
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
            blue = Mathf.Min(5, blue + pointsToAdd);
            blueText.text = "Points: " + blue;
            Destroy(parentPrefab);
        }
        if (parentPrefab != null && parentPrefab.tag.Equals("obstacle"))
        {
            if (collisionTimeout == 10 && movementScript.playerState != PlayerMovement.PlayerState.NEUTRAL)
            {
                if (collisionTimeout == 10 && blueShield)
                {
                    Destroy(parentPrefab);
                    blueShield = false;
                }
                else if(collisionTimeout == 10)
                {
                    Destroy(parentPrefab);
                    movementScript.playerState = PlayerMovement.PlayerState.NEUTRAL;
                }
                collisionTimeout = 0;
            }
            else
            {
                if(collisionTimeout == 10)
                    SceneManager.LoadScene("EndScene");
            }
            
        }
    }
}
