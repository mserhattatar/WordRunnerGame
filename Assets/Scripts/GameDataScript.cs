using System;
using System.IO;
using UnityEngine;

public static class GameDataScript
{
    public static void SetLevelDataAsJson()
    {
        string path = Application.persistentDataPath + "/RunRunGameData.json";
        var data = SerializeMapData();

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(data);
            }
        }
    }

    private static string SerializeMapData()
    {
        var levelData = new LevelData();
        levelData.levelNumber = GameManager.instance.levelNumber;
        var data = JsonUtility.ToJson(levelData);
        return data;
    }

    public static void GetLevelDataFromJson()
    {
        string path = Application.persistentDataPath + "/RunRunGameData.json";
        var data = ReadDataFromText(path);
        var levelData = JsonUtility.FromJson<LevelData>(data);
        GetLevelNumberData(levelData);
    }

    private static string ReadDataFromText(string path)
    {
        string data = null;
        try
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                data = reader.ReadToEnd();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }

        return data;
    }

    private static void GetLevelNumberData(LevelData levelData)
    {
        var data = new LevelData();
        data = levelData;
        if (data != null)
            GameManager.instance.levelNumber = data.levelNumber;
        else
        {
            Debug.Log("data bulunamadı");
        }
    }
}

[Serializable]
public class LevelData
{
    public int levelNumber;
}