using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static bool isMuted = false;
    public AudioSource mainMenuTrack;
    private Scene currentScene;
    public GameObject[] musicObj;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }


    private void Awake()
    {
        musicObj = GameObject.FindGameObjectsWithTag("BackgroundMenuMusic");
        if (musicObj.Length > 1)
        {
            if (this.gameObject.name != "Player")
                Destroy(this.gameObject);
            else
                this.gameObject.SetActive(false);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isMuted);
        if (isMuted)
        {
            musicObj[0].SetActive(false);
        }
        else
        {
            musicObj[0].SetActive(true);
        }
        if (currentScene.name == "GameScene")
        {
            musicObj[0].SetActive(false);
        }
        else
        {
            musicObj[0].gameObject.SetActive(true);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        Debug.Log(isMuted);
    }

}
