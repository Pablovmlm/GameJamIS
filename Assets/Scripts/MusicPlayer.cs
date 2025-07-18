using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Hace que sobreviva al cambio de escena
        }
        else
        {
            Destroy(gameObject); // Evita duplicados si ya hay uno
        }
    }
}
