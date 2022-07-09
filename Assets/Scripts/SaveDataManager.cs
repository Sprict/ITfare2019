using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SaveDataManager
{
    const string SAVE_FILE = "save_data.json";
    const string DATA_DIR = "Assets/StreamingAssets/data/";
    static string saveDataPath;

    public static SaveData saveData;


    public static void Init()
    {
        saveDataPath = Path.Combine(DATA_DIR + SAVE_FILE);
        Load();
    }

    public static void Load()
    {
        if (File.Exists(saveDataPath))
        {
            FileStream stream = File.Open(saveDataPath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            reader.Close();
            stream.Close();

            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
            saveData.Init();
            Save();
        }
    }
    public static void Save()
    {
        if (!Directory.Exists(DATA_DIR))
        {
            Directory.CreateDirectory(DATA_DIR);
        }

        var json = JsonUtility.ToJson(saveData);

        StreamWriter writer = new StreamWriter(saveDataPath, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }
}


[Serializable]
public class SaveData
{
    public string[] name = new string[100];
    public int[] score = new int[100];

    public void SetName(string name, int index) { this.name[index] = name; }
    public void SetScore(int score, int index) { this.score[index] = score; }

    public void Init()
    {
        for (int i = 0; i < 100; i++)
        {
            name[i] = "名無し";
            score[i] = 0;
        }
    }
}