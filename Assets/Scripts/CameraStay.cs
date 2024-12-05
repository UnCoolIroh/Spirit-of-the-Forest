using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraStay : MonoBehaviour
{
    public static CameraStay instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
