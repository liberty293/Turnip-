using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFood : MonoBehaviour
{
    public GameObject leaf1;
    public GameObject leaf2;
    public GameObject leaf3;
    public void Drop()
    {
        leaf1.GetComponent<Rigidbody>().useGravity = true;
        leaf2.GetComponent<Rigidbody>().useGravity = true;
        leaf3.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        //then play animation
    }
    public void Reset()
    {

    }
}
