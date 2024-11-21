using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject advanceButton;
    public int sceneBuildIndex;
    //AudioManager audioManager;
    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (sceneBuildIndex == 3)
            {
                //audioManager.playSFX(audioManager.door);
                //audioManager.playMusic();
            }
            else if (sceneBuildIndex == 4)
            {
                //audioManager.playSFX(audioManager.hit);
                //audioManager.pause();
            }
            else
            {
                //audioManager.playSFX(audioManager.door);
            }
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
