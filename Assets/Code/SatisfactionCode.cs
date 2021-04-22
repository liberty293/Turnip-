using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SatisfactionCode : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public GameObject turnip;
    public GameObject thirst;
    public GameObject hunger;
    public bool chomped = false;
    private IEnumerator slowlySat;
    private bool increasing;
    public SaveData saveData;
    // Start is called before the first frame update
    void onAwaken()
    {
        slide.value = saveData.satisfaction;
        chomped = saveData.chomped;
    }

    public void Feed(float amount)
    {
        if (!increasing)
        {
            increasing = true;
            if (slide.value >= 1)
            {
                Consume();
            }
            else
            {
                slowlySat = SlowlyIncSat(amount, 3, 2);//replace with animation duration
                hunger.GetComponent<Hunger>().Feed(amount);
                StartCoroutine(slowlySat);
            }
            increasing = false;
        }
    }
    public void Water(float amount)
    {
        if (!increasing)
        {
            increasing = true;
            slowlySat = SlowlyIncSat(amount, 6, 2);
            thirst.GetComponent<Thirst>().Water(amount);
            StartCoroutine(slowlySat);
            increasing = false;
        }
    }
    private IEnumerator SlowlyIncSat(float amount, float time, float delay)
    {
            float timeElapsed = 0;
            while (timeElapsed < delay)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                yield return null;
            }
            timeElapsed = 0;
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_eating.mat", typeof(Material));
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                yield return null;
            }
            turnip.GetComponent<MeshRenderer>().material = (Material) AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_default.mat", typeof(Material));
    }
    private void Consume()
    {
        slide.value = 0;
        turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_scared.mat", typeof(Material));
        //turnipAnimator.SetTrigger(getEaten);
        //turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));
    }
}
