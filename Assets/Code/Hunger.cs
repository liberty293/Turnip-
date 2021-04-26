using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Hunger : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public float hungerPerSec = 0.08f;
    private IEnumerator slowly;
    private IEnumerator slideL;
    bool increasing = false;
    public SaveData saveData;
    public GameObject turnip;
    public Animator anim;
    public DropFood drop;
    protected bool alive = true;
    void Awake()
    {
        slide.value = saveData.hunger;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (turnip.GetComponent<alive>().living) {
            slide.value -= Time.deltaTime * hungerPerSec;
            if (slide.value <= 0)
            {
                //malnourish
                turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead_malnourishment.mat", typeof(Material));
                turnip.GetComponent<alive>().living = false;
            }
        }
    }
    public void Feed(float amount)
    {
        slowly = SlowlyInc(amount, 2.5f, 2.7f);
        StartCoroutine(slowly);
    }
    private IEnumerator SlowlyInc(float amount, float time, float delay)
    {
        if (!increasing)
        {
            increasing = true;
            drop.Drop();
            float timeElapsed = 0;
            while (timeElapsed < delay)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, delay);
                yield return null;
            }
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_begin_eat.mat", typeof(Material));
            anim.Play("Leaf1");
            timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                if (timeElapsed >= 1.6)
                {
                    drop.SetVis(2,false);
                }
                else if (timeElapsed >= 1.0)
                {
                    drop.SetVis(1, false);
                }
                else if (timeElapsed >= .3)
                {
                    drop.SetVis(3, false);
                }
                yield return null;
            }
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_eating.mat", typeof(Material));
            slideL = drop.slideLeft();
            yield return StartCoroutine(slideL);
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_default.mat", typeof(Material));
            increasing = false;
            drop.Remove();
        }

    }
}
