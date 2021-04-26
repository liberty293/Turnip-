using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGame : MonoBehaviour
{
    public SaveData data;
    // Start is called before the first frame update
    public void toGame()
    {
        if (data.chomped || data.thirst <= 0 || data.hunger <= 0)
        {
            //reset
            data.chomped = false;
            data.thirst = 1;
            data.hunger = 1;
            data.satisfaction = 0;
        }
        SceneManager.LoadScene("SampleScene");
    }
}
