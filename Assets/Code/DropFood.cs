using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DropFood : MonoBehaviour
{
    public GameObject Plate;
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    private Vector3 pos;
    private Quaternion quat;
    private Vector3 l1p;
    private Quaternion l1q;
    private Vector3 l2p;
    private Quaternion l2q;
    private Vector3 l3p;
    private Quaternion l3q;
    public void Start()
    {
        pos = Plate.transform.position;
        quat = Plate.transform.rotation;
        l1p = L1.transform.position;
        l1q = L1.transform.rotation;
        l2p = L2.transform.position;
        l2q = L2.transform.rotation;
        l3p = L3.transform.position;
        l3q = L3.transform.rotation;
}
    public void Drop()
    {
        Reset();
        Plate.GetComponent<Rigidbody>().useGravity = true;
    }
    public void Reset()
    {
        Plate.transform.position = pos;
        Plate.transform.rotation = quat;
        L1.transform.position = l1p;
        L1.transform.rotation = l1q;
        L2.transform.position = l2p;
        L2.transform.rotation = l2q;
        L3.transform.position = l3p;
        L3.transform.rotation = l3q;

        L1.SetActive(true);
        L2.SetActive(true);
        L3.SetActive(true);

    }
    public void Remove()
    {
        Plate.transform.position = new Vector3(0f,-10f,0f);

        Plate.GetComponent<Rigidbody>().useGravity = false;
    }
    public IEnumerator slideLeft()
    {
        while (Plate.transform.position.z > -68)
        {
            Plate.transform.position = new Vector3(Plate.transform.position.x, Plate.transform.position.y, Plate.transform.position.z - Time.deltaTime*40);
            yield return null;
        }
        Reset();
    }
    public void SetVis(int leaf, bool on)
    {
        if (leaf == 1)
        {
            L1.SetActive(on);
        }
        if (leaf == 2)
        {
            L2.SetActive(on);
        }
        if (leaf == 3)
        {
            L3.SetActive(on);
        }
    }
}
