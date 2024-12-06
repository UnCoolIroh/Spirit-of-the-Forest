using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    public bool isPaused;
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UserInput.instance.controls.Gameplay.Pause.WasPressedThisFrame())
        {
            if (SceneManager.GetActiveScene().buildIndex == 5) {
                GoToHub();
            }
            else if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToHub()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().healthReset();
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.PickupCoin(-player.coins);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
