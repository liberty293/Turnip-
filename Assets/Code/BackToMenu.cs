using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
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
        data.Save(satisfaction, hunger, thirst);
        EditorUtility.SetDirty(data);
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
            turnip.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/TurnipModel/assets/face_textures/Materials/texture_uwu.mat", typeof(Material));
            yield return new WaitForSecondsRealtime(1.2f);
        }
        WriteToFile();
        SceneManager.LoadScene("Title Screen");
    }
}
