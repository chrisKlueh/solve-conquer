using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static bool gameLoaded = false;

    public static void SaveGame(MovingPlatformNew plat,string stage,string lvl, bool saveCompanionPosition)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(plat, stage, lvl, saveCompanionPosition);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.dat";
        if (File.Exists(path))
        {
            gameLoaded = true;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;

        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
