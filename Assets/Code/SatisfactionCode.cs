using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SatisfactionCode : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public GameObject turnip;
    private bool chomped;
    private IEnumerator slowlySat;
    private bool increasing;
    // Start is called before the first frame update
    void Start()
    {
        chomped = false;
    }

    public void Feed(float amount)
    {
        if (slide.value >= 1)
        {
            Consume();
        }
        else
        {
            slowlySat = SlowlyIncSat(amount, 3, 2);//replace with animation duration
            StartCoroutine(slowlySat);
        }
    }
    public void Water(float amount)
    {

        slowlySat = SlowlyIncSat(amount, 6, 2);
        StartCoroutine(slowlySat);
    }
    private IEnumerator SlowlyIncSat(float amount, float time, float delay)
    {
        if (!increasing) {
            increasing = true;
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
                turnip.transform.localScale.Scale(new Vector3(1+Time.deltaTime,1+ Time.deltaTime, 1+ Time.deltaTime));
                yield return null;
            }
            increasing = false;
            turnip.GetComponent<MeshRenderer>().material = (Material) AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_default.mat", typeof(Material));
        }

    }
    private void Consume()
    {
        slide.value = 0;
        turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_scared.mat", typeof(Material));
        //turnipAnimator.SetTrigger(getEaten);
        //turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));
    }
}
