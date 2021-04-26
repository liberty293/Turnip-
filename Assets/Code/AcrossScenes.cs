using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrossScenes : MonoBehaviour
{
    public AudioSource music;
    static bool started = false;
    private void Start()
    {
        if (!started)
        {
            music.Play();
            started = true;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
