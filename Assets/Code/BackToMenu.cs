using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public GameObject hunger;
    public GameObject thirst;
    public SaveData data;
    public GameObject satisfaction;
    public GameObject turnip;
    public Animator anim;
    public void WriteToFile()
    {
        data.satisfaction = satisfaction.GetComponent<UnityEngine.UI.Slider>().value;
        data.chomped = satisfaction.GetComponent<SatisfactionCode>().chomped;
        data.thirst = thirst.GetComponent<UnityEngine.UI.Slider>().value;
        data.hunger = hunger.GetComponent<UnityEngine.UI.Slider>().value;
        if (data.chomped)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/turnip.turnip";
            FileStream stream = new FileStream(path, FileMode.Create);
            data.Save(satisfaction, hunger, thirst);

            formatter.Serialize(stream, new SerialData(data));
            stream.Close();
        }
    }
    public void toMenu()
    {

        StartCoroutine(Sleepy());
    }
    IEnumerator Sleepy()
    {
        if (turnip.GetComponent<alive>().living) {
            anim.Play("Drop Into Ground");
            turnip.GetComponent<MeshRenderer>().material = Resources.Load<Material>("texture_uwu");
            yield return new WaitForSecondsRealtime(1.2f);
        }
        WriteToFile();
        SceneManager.LoadScene("Title Screen");
    }
}
