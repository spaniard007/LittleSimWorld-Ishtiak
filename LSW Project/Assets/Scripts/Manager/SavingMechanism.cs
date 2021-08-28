using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingMechanism
{
    readonly static string fileName = "GameData.lsw"; //file format with name
    public static void SaveData(Dress_Bought_List data)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/"+ fileName;
        Debug.Log("Saved to: "+path);
        FileStream stream = new FileStream(path,FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Dress_Bought_List LoadPlayer()
    {
        string path= Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            Dress_Bought_List data = formatter.Deserialize(stream) as Dress_Bought_List;
            stream.Close();
            Debug.Log("Loaded from: " + path);

            return data;
        }
        else
        {
            Debug.Log("There is no file in "+path);
            return null;
        }
    }
}
