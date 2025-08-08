using UnityEngine;
using System.IO;
public static class JSONLoader
{
    /// <summary>
    /// Loads levels from StreamingAssets/levelData.json
    /// </summary>
    public static DataRaw[] LoadLevels(string fileName= "levelData")
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
        if (!File.Exists(path))
        {
            Debug.LogError($"Level data file not found at {path}");
            return new DataRaw[0];
        }
        string json = File.ReadAllText(path);
        LevelCollection coll = JsonUtility.FromJson<LevelCollection>(json);
        return coll.data;
    }

    /// <summary>
    /// Loads data from StreamingAssets/endlessData.json
    /// </summary>
    public static DataRaw LoadEndless(string fileName = "endlessData")
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
        if (!File.Exists(path))
        {
            Debug.LogError($"Endless data file not found at {path}");
            return null;
        }
        string json = File.ReadAllText(path);
        // Directly deserialize into your EndlessDataRaw class
        DataRaw data = JsonUtility.FromJson<DataRaw>(json);
        return data;
    }
}