using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questName;
    public int targetCount;
}