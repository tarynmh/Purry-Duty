using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UserStateNS;

// File IO applications from: https://www.youtube.com/watch?v=XOjd_qU2Ido

public static class SaveSystem
{
    public static void SavePlayer(UserState user) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, user);
        stream.Close();
        Debug.Log("Game Saved.");
    }

    public static UserState LoadPlayer()
    {
        string path = Application.persistentDataPath + "/user.data";
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserState user = formatter.Deserialize(stream) as UserState;
            stream.Close();
            Debug.Log("Game loaded.");

            return user;
        } 
        else {
            Debug.LogError("Save file not found. File path: " + path);
            return null;
        }
    }
}
