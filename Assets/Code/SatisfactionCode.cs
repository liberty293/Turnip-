using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SatisfactionCode : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public GameObject turnip;
    public GameObject thirst;
    public GameObject hunger;
    public bool chomped = false;
    private IEnumerator slowlySat;
    private static bool increasing;
    public GameObject rain;
    public SaveData saveData;
    public GameObject image;
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

        }
    }
    public void Water(float amount)
    {
        if (!increasing)
        {
            rain.GetComponent<ParticleSystem>().Play();
            increasing = true;
            slowlySat = SlowlyIncSat(amount, 6, 2);
            thirst.GetComponent<Thirst>().Water(amount);
            StartCoroutine(slowlySat);
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
        if (slide.value >= 1)
        {
            image.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/ui_2.png", typeof(Sprite));
        }
        increasing = false;
    }
    private void Consume()
    {
        turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_scared.mat", typeof(Material));
        //turnipAnimator.SetTrigger(getEaten);
        //turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));
    }
}
