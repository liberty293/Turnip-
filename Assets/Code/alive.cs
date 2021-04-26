using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alive : MonoBehaviour
{
    public SaveData data;
    public bool living;
    private void Awake()
    {
        living = !data.chomped;
    }
}
