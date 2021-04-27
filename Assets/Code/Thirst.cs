using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Thirst : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public float thirstPerSec = 0.05f;
    private IEnumerator slowly;
    bool increasing = false;
    public SaveData saveData;
    public GameObject turnip;
    void Awake()
    {
        increasing = false;
        slide.value = saveData.thirst;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (turnip.GetComponent<alive>().living) {
            slide.value -= Time.deltaTime * thirstPerSec;
            if (slide.value <= 0)
            {
                //malnourish
                turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead_malnourishment.mat", typeof(Material));
                turnip.GetComponent<alive>().living = false;
            }
        }
    }
    public void Water(float amount)
    {
        slowly = SlowlyInc(amount, 6, 2.4f);
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
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, delay);
                yield return null;
            }
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_eating.mat", typeof(Material));
            timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                yield return null;
            }
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_default.mat", typeof(Material));
            increasing = false;
        }

    }
}
