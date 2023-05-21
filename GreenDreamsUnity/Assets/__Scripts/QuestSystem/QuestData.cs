using UnityEngine;

public enum QuestType
{
    Build,
    Collect,
    UseItem
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    public QuestType questType;
    public BuildingType wantedBuildingType;

    public string questName;
    public int targetCount;
    public int currentCount;
    public bool isCompleted;
}