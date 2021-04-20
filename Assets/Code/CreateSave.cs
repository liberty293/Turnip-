using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CreateSave : MonoBehaviour
{
    public static void SaveTurnip(GameObject satisfaction)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/turnip.turnip";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(satisfaction);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadTurnip()
    {
        string path = Application.persistentDataPath + "/turnip.turnip";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
