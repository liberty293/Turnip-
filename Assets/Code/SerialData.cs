using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class SerialData
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
    public SerialData(SaveData save)
    {
        satisfaction = save.satisfaction;
        chomped = save.chomped;
        thirst = save.thirst;
        hunger = save.hunger;
    }
}
