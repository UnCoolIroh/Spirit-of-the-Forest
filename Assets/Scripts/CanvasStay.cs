using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasStay : MonoBehaviour
{
    public static CanvasStay instance;

    private void Awake()
    {
        if (instance == null)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
