using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public PlayerController player;
    public GameObject PlayerObject;
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        player = PlayerObject.GetComponent<PlayerController>();
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoBackToHub()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        PlayerObject.transform.position = Vector3.zero;
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
        PlayerObject.GetComponent<PlayerHealth>().healthReset();
        player.PickupCoin(-player.coins);
        player.isAlive = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
