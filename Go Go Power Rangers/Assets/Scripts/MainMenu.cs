using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsMenu;
    public GameObject howToPlayMenu;
    public GameObject optionsMenu;
    public AudioController audioScript;

    public void Start()
    {
        if(creditsMenu != null)
        {
            creditsMenu.SetActive(false);
            howToPlayMenu.SetActive(false);
        }
        if (audioScript && audioScript.musicObj[0])
            audioScript.musicObj[0].SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (audioScript && audioScript.musicObj[0])
                audioScript.musicObj[0].SetActive(false);
        }
    }

    public void GoToMenu()
    {
        if (audioScript && audioScript.musicObj[0])
        {
            Debug.Log("in");
            for(int i = 0; i < audioScript.musicObj.Length; i++)
            {
                audioScript.musicObj[i].SetActive(false);
            }
        }
        SceneManager.LoadScene("StartScene");
        if(optionsMenu != null )
        {
            optionsMenu.SetActive(true);
            creditsMenu.SetActive(false);
            howToPlayMenu.SetActive(false);
        }
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void GoToHowToPlay()
    {
        howToPlayMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void GoToCredits()
    {
        creditsMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
