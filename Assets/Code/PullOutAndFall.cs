using System;
using System.Collections;
using UnityEditor;
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
    public GameObject turnip;
    public GameObject bitModel;
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
       // StartCoroutine(StartAnim());
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
        rbody.isKinematic = false;
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
        turnip.transform.Rotate(new Vector3(0,30,0));
        TurnipGroup.transform.position = new Vector3(TurnipGroup.transform.position.x, TurnipGroup.transform.position.y, TurnipGroup.transform.position.z+3);
        turnip.GetComponent<alive>().living = false;
        turnip.GetComponent<MeshFilter>().mesh = bitModel.GetComponent<MeshFilter>().mesh;
        turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_dead.mat", typeof(Material));

    }
}
