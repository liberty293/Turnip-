using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public void causeRain()
    {
        this.GetComponent<ParticleSystem>().Play();
    }
}
