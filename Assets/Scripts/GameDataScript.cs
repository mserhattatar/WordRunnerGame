using System;
using System.IO;
using UnityEngine;

public static class GameDataScript
{
    public static void SetLevelDataAsJson()
    {
        string path = Application.persistentDataPath + "/WordRunner.json";
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
        var levelData = new LevelData
        {
            levelNumber = GameManager.instance.levelNumber,
            language = GameManager.instance.language
        };
        var data = JsonUtility.ToJson(levelData);
        return data;
    }

    public static void GetLevelDataFromJson()
    {
        string path = Application.persistentDataPath + "/WordRunner.json";
        var data = ReadDataFromText(path);
        var levelData = JsonUtility.FromJson<LevelData>(data);
        GetLevelNumberData(levelData);
    }

    private static string ReadDataFromText(string path)
    {
        string data = null;
        try
        {
            using var fs = new FileStream(path, FileMode.Open);
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
        if (levelData != null)
        {
            GameManager.instance.levelNumber = levelData.levelNumber;
            GameManager.instance.language = levelData.language;
        }

        else
            Debug.Log("data bulunamadı");
    }
}

[Serializable]
public class LevelData
{
    public int levelNumber;
    public int language;
}