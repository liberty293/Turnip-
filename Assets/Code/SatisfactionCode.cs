using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionCode : MonoBehaviour
{
    public UnityEngine.UI.Slider slide;
    public Animator turnipAnimator;
    private bool chomped;
    // Start is called before the first frame update
    void Start()
    {
        chomped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseSatisfaction(float num)
    {
        if (chomped == false) {
            slide.value += num;
        }
        if (slide.value == 1)
        {
            chomped = true;
            Consume();
        }
    }
    private void Consume()
    {
        slide.value = 0;
        //turnipAnimator.SetTrigger(getEaten);
    }
}
