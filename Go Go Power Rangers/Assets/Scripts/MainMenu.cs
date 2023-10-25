using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsMenu;
    public GameObject howToPlayMenu;
    public GameObject optionsMenu;

    public void Start()
    {
        if(creditsMenu != null)
        {
            creditsMenu.SetActive(false);
            howToPlayMenu.SetActive(false);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
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
