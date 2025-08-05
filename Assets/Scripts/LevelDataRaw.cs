using UnityEngine;
[System.Serializable]
public class LevelDataRaw
{
    public int bugCount;
    public int wordCount;
    public int timeSec;
    public int totalScore;
    public Vector2Int gridSize;
    public TileData[] gridData;
}