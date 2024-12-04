using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public int sceneBuildIndex;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
