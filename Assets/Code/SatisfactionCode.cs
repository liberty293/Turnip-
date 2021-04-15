using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        slowlySat = SlowlyIncSat(amount, 3);//replace with animation duration
        StartCoroutine(slowlySat);
    }
    public void Water(float amount)
    {

        slowlySat = SlowlyIncSat(amount, 7);
        StartCoroutine(slowlySat);
    }
    private IEnumerator SlowlyIncSat(float amount, float time)
    {
        if (!increasing) {
            increasing = true;
            float timeElapsed = 0;
            while (timeElapsed < time)
            {
                timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, time);
                slide.value += Time.deltaTime * amount / time;
                turnip.transform.localScale.Scale(new Vector3(1+Time.deltaTime,1+ Time.deltaTime, 1+ Time.deltaTime));
                yield return null;
            }
            increasing = false;
        }

    }
    private void Consume()
    {
        slide.value = 0;
        //turnipAnimator.SetTrigger(getEaten);
    }
}
