using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class SaveData
{
    public static void CreateFile()
    {
        File.Create(Application.streamingAssetsPath + "/data.stuff");
    }

    public static void SaveGame(Dictionary<string,UserData> data) 
    {
        //saves the player data to a binary file
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.streamingAssetsPath + "/data.stuff";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Dictionary<string, UserData> LoadData() 
    {
        //loads player data from save file and returns a dictionary
        Dictionary<string, UserData> users = new Dictionary<string, UserData>();
        string path = Application.streamingAssetsPath + "/data.stuff";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            users = (Dictionary<string,UserData>)formatter.Deserialize(stream);
            stream.Close();

            return users;
        }
        else
        {
            return null;
        }
    }
}
