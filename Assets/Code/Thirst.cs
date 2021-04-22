﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thirst : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public float thirstPerSec = 0.05f;
    private IEnumerator slowly;
    bool increasing = false;
    public SaveData saveData;
    void onAwaken()
    {
        slide.value = saveData.thirst;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        slide.value -= Time.deltaTime * thirstPerSec;
    }
    public void Water(float amount)
    {
        slowly = SlowlyInc(amount, 6, 2);
        StartCoroutine(slowly);
    }
    private IEnumerator SlowlyInc(float amount, float time, float delay)
    {
        if (!increasing)
        {
            increasing = true;
            float timeElapsed = 0;
            while (timeElapsed < delay)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                yield return null;
            }
            timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                yield return null;
            }
            increasing = false;
        }

    }
}