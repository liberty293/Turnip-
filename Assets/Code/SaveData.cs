﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Save", order = 1), Serializable]
public class SaveData : ScriptableObject
{
    public float satisfaction;
    public float thirst;
    public float hunger;
    public bool chomped;

    public void Save(GameObject satisfactionVal, GameObject hungerVal, GameObject thirstVal)
    {
        satisfaction = satisfactionVal.GetComponent<UnityEngine.UI.Slider>().value;
        chomped = satisfactionVal.GetComponent<SatisfactionCode>().chomped;
        thirst = thirstVal.GetComponent<UnityEngine.UI.Slider>().value;
        hunger = hungerVal.GetComponent<UnityEngine.UI.Slider>().value;
    }
    public SaveData Save(SerialData save)
    {
        satisfaction = save.satisfaction;
        chomped = save.chomped;
        thirst = save.thirst;
        hunger = save.hunger;
        return this;
    }
}
