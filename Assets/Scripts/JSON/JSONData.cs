using UnityEngine;
[System.Serializable]
public class LevelCollection
{
    public DataRaw[] data;
}

[System.Serializable]
public class DataRaw
{
    public int bugCount;
    public int wordCount;
    public int timeSec;
    public int totalScore;
    public Vector2Int gridSize;
    public TileData[] gridData;
}
