using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Code heavily referenced from this video by RehopeGames: https://www.youtube.com/watch?v=N8whM1GjH4w&ab_channel=RehopeGames.
*/
public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip buttonPress;
    public AudioClip music;
    public AudioClip bear;
    public AudioClip whoosh;
    public AudioClip sword;
    public AudioClip hit;
   
    private static AudioManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playMusic(music);
    }

    public void playSFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }

    public void playMusic(AudioClip clip) {
        musicSource.PlayOneShot(clip);
    }

    public void pause() {
        musicSource.Pause();
    }
   
    public void unpause() {
        musicSource.UnPause();
    }
}
