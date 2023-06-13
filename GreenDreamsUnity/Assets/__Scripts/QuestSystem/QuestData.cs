using UnityEngine;

public enum QuestType
{
    Build,
    Collect,
    UseItem,
    Talk,
    Farm
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    public QuestType questType;
    public BuildingType wantedBuildingType;

    public string questName;
    public string questDescription;
    public int targetCount;
    public int currentCount;
    public bool isCompleted;
    
}