using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	public bool isPaused;
    public Image backgroundImage;
    public AudioSource pauseMenuAudio;
    public AudioSource backgroundAudio;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        backgroundImage.enabled = false;
        backgroundAudio.Play();
        pauseMenuAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
	{
		if(isPaused)
			ResumeGame();
		else
			PauseGame();
	}
    }

    public void PauseGame()
    {
	    pauseMenu.SetActive(true);
	    Time.timeScale = 0f;
	    isPaused = true;
        backgroundImage.enabled = true;
        backgroundAudio.Pause();
        pauseMenuAudio.Play();
    }

    public void ResumeGame()
    {
	    pauseMenu.SetActive(false);
	    Time.timeScale = 1f;
	    isPaused = false;
        backgroundImage.enabled = false;
        backgroundAudio.Play();
        pauseMenuAudio.Stop();
    }
	    
    public void GoToMainMenu()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            backgroundImage.enabled = false;
            backgroundAudio.Play();
            pauseMenuAudio.Stop();
        }
        SceneManager.LoadScene("StartScene");
    }
}
