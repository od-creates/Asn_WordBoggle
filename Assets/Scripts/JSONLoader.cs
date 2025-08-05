using UnityEngine;
using System.IO;
public static class JSONLoader
{
    /// <summary>
    /// Loads levels from StreamingAssets/levelData.json
    /// </summary>
    public static LevelDataRaw[] LoadLevels(string fileName = "levelData")
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
        if (!File.Exists(path))
        {
            Debug.LogError($"Level data file not found at {path}");
            return new LevelDataRaw[0];
        }
        string json = File.ReadAllText(path);
        LevelCollection coll = JsonUtility.FromJson<LevelCollection>(json);
        return coll.data;
    }
}