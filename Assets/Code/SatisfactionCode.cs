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
    public Animator turnipAnimator;
    public GameObject fullTurnip;
    public GameObject music;
    public GameObject bitModel;
    // Start is called before the first frame update
    void Awake()
    {
        slide.value = saveData.satisfaction;
        chomped = saveData.chomped;
        if (chomped) {
            turnip.transform.Rotate(new Vector3(0, 30, 0));
            turnip.GetComponent<alive>().living = false;
            turnip.GetComponent<MeshFilter>().mesh = bitModel.GetComponent<MeshFilter>().mesh;
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));
        }
    }
    public void Feed(float amount)
    {
        if (!increasing && turnip.GetComponent<alive>().living)
        {
            increasing = true;
            if (slide.value >= 1)
            {
                Consume();
            }
            else
            {
                slowlySat = SlowlyIncSat(amount, 4, 2.7f);//replace with animation duration
                hunger.GetComponent<Hunger>().Feed(amount);
                StartCoroutine(slowlySat);
            }

        }
    }
    public void Water(float amount)
    {
        if (!increasing && turnip.GetComponent<alive>().living)
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
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, delay);
                yield return null;
            }
            timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                yield return null;
            }
            //turnip.GetComponent<MeshRenderer>().material = (Material) AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_default.mat", typeof(Material));
        if (slide.value >= 1)
        {
            image.GetComponent<Image>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/ui_2.png", typeof(Sprite));
        }
        increasing = false;
    }
    private void Consume()
    {
        turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_scared.mat", typeof(Material));
        music.GetComponent<AudioSource>().mute = true;
        turnipAnimator.Play("Pulled");
        chomped = true;
        slide.value = 0;
        fullTurnip.GetComponent<PullOutAndFall>().PullNFall();
        //turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));
    }
}
