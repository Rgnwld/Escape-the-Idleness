using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (Player player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerInfo.esfrid";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerInfo data = new PlayerInfo(player);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Player Saved");
    }

    public static PlayerInfo LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/playerInfo.esfrid";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerInfo data = binaryFormatter.Deserialize(stream) as PlayerInfo;
            stream.Close();

            Debug.Log("Player Loaded");
            return data;
        }
        else
        {
            Debug.LogWarning("No Save at: " + path);
            return null;
        }

    }
}
