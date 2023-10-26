using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsMenu;
    public GameObject howToPlayMenu;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public AudioSource mainMenuTrack;

    public bool isMuted = false;

    public void Start()
    {
        if(creditsMenu != null)
        {
            creditsMenu.SetActive(false);
            howToPlayMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
    }

    public void PlayGame()
    {
        mainMenuTrack.Stop();
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GoToOptions()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        optionsMenu.SetActive(true);
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

    public void GoToMenuBack()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            mainMenuTrack.Pause();
        }
        else
        {
            mainMenuTrack.Play();
        }
    }

}
