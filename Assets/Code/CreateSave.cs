using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CreateSave : MonoBehaviour
{
    public GameObject hunger;
    public GameObject thirst;
    public SaveData data;
    public void WriteToFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/turnip.turnip";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, new SerialData(data));
        stream.Close();
    }
    public void SaveTurnip(GameObject satisfaction)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/turnip.turnip";
        FileStream stream = new FileStream(path, FileMode.Create);
        data.Save(satisfaction, hunger, thirst);

        formatter.Serialize(stream, new SerialData(data));
        stream.Close();
    }
    public SaveData LoadTurnip()
    {
        string path = Application.persistentDataPath + "/turnip.turnip";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data.Save(formatter.Deserialize(stream) as SerialData);
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
