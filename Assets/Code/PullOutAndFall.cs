using System;
using System.Collections;
using UnityEngine;

public class PullOutAndFall : MonoBehaviour
{
    //Attach this script to the turnip group. Call PullNFall() to pull out of ground
    [Tooltip("Attach animator that has pull animation")]
    public Animator TurnipAnim;
    [Tooltip("Choose a height sufficiently off screen. This is also the height the turnip will drop from")]
    public float OffScreenHeight;
    [Tooltip("The spped turnip falls")]
    public float speeddown = 60;
    [SerializeField]
    public float minT, maxT;
    private GameObject TurnipGroup;
    BoxCollider bcollider;
    Rigidbody rbody;
    bool fall = false;
    public float SecondsInAir = 3f;
    public void PullNFall()
    {
        StartCoroutine(StartAnim());
    }
    private void Update()
    {


    }
    private void Start()
    {
 
        rbody = GetComponent<Rigidbody>();
        bcollider = GetComponent<BoxCollider>();
        TurnipGroup = gameObject;
        StartCoroutine(StartAnim());
    }

    IEnumerator StartAnim()
    {
        bcollider.enabled = false;
        rbody.useGravity = false;
        yield return PullAnim();
        yield return new WaitForSeconds(SecondsInAir);
        rbody.useGravity = true;
        bcollider.enabled = true;
        fall = true; 
    }
    void FixedUpdate()
    {
        if (!fall) return;
        rbody.AddTorque(new Vector3(UnityEngine.Random.Range(minT, maxT), UnityEngine.Random.Range(minT, maxT), UnityEngine.Random.Range(minT, maxT)));
        rbody.AddForce(Vector3.down * speeddown) ;
    }
    
    private IEnumerator PullAnim()
    {
        Debug.Log("I started");
        TurnipAnim.Play("Pulled");
        yield return new WaitWhile(() => TurnipAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
        TurnipAnim.enabled = false;
        while (TurnipGroup.transform.position.y < OffScreenHeight)
        {
            TurnipGroup.transform.Translate(Vector3.up, Space.World);
            yield return null;
        }


    }
}
