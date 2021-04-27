using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        increasing = false;

        if (chomped) {
            turnip.transform.Rotate(new Vector3(0, 30, 0));
            turnip.GetComponent<alive>().living = false;
            turnip.GetComponent<MeshFilter>().mesh = bitModel.GetComponent<MeshFilter>().mesh;
            turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_dead");
        }
        else if(hunger.GetComponent<Hunger>().slide.value >= 0 && thirst.GetComponent<Thirst>().slide.value >= 0)
        {
            turnip.GetComponent<alive>().living = true;
        }
        if (slide.value >= 1)
        {
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui_2");
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
                slowlySat = SlowlyIncSat(amount, 4f, 2.7f);//replace with animation duration
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
            image.GetComponent<Image>().sprite = Resources.Load<Sprite>("ui_2");
        }
        increasing = false;
    }
    private void Consume()
    {
        turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_scared");
        music = GameObject.FindGameObjectsWithTag("Music")[0];
        music.GetComponent<AudioSource>().mute = true;
        turnipAnimator.Play("Pulled");
        chomped = true;
        slide.value = 0;
        fullTurnip.GetComponent<PullOutAndFall>().PullNFall();
        //turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));
    }
}
