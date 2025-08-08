using UnityEngine;

public static class JSONLoader
{
    /// <summary>
    /// Loads all levels from Resources/levelData.json
    /// </summary>
    public static DataRaw[] LoadLevels(string fileName = "levelData")
    {
        // Load the TextAsset from Resources (no extension)
        TextAsset txt = Resources.Load<TextAsset>(fileName);
        if (txt == null)
        {
            Debug.LogError($"Level data file not found in Resources/{fileName}.json");
            return new DataRaw[0];
        }

        // Parse into your wrapper
        LevelCollection coll = JsonUtility.FromJson<LevelCollection>(txt.text);
        if (coll == null || coll.data == null)
        {
            Debug.LogError($"Failed to parse levels from Resources/{fileName}.json");
            return new DataRaw[0];
        }

        return coll.data;
    }

    /// <summary>
    /// Loads the endless mode data from Resources/endlessData.json
    /// </summary>
    public static DataRaw LoadEndless(string fileName = "endlessData")
    {
        TextAsset txt = Resources.Load<TextAsset>(fileName);
        if (txt == null)
        {
            Debug.LogError($"Endless data file not found in Resources/{fileName}.json");
            return null;
        }

        DataRaw data = JsonUtility.FromJson<DataRaw>(txt.text);
        if (data == null)
        {
            Debug.LogError($"Failed to parse endless data from Resources/{fileName}.json");
        }
        return data;
    }
}