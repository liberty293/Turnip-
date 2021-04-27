using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrossScenes : MonoBehaviour
{
    public AudioSource music;
    public static bool started = false;
    public SaveData save;

    private void Awake()
    {
        if (!started) {
            DontDestroyOnLoad(this.gameObject);
            music.Play();
            music.mute = save.chomped;
            started = true;
            GameObject[] other = GameObject.FindGameObjectsWithTag("Music");
            foreach (GameObject otherPlayer in other)
            {
                if (!otherPlayer.Equals(this.gameObject))
                {
                    Destroy(otherPlayer);
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
