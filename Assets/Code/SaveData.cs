using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveData : MonoBehaviour
{
    public float satisfaction;

    public SaveData(GameObject satisfactionVal)
    {
        satisfaction = satisfactionVal.GetComponent<UnityEngine.UI.Slider>().value;
    }
}
