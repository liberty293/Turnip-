using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        increasing = false;
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
                turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_dead_malnourishment");
                turnip.GetComponent<alive>().living = false;
            }
        }
    }
    public void Feed(float amount)
    {
        slowly = SlowlyInc(amount, 1.8f, 2.7f);
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
            turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_begin_eat");
            anim.Play("Leaf1");
            timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                if (timeElapsed >= 1.4)
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
            turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_eating");
            slideL = drop.slideLeft();
            yield return StartCoroutine(slideL);
            turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_default");
            increasing = false;
            drop.Remove();
        }

    }
}
