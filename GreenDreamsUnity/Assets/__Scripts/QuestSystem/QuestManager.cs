using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> quests; // Quest listesini Inspector'dan doldurun
    public QuestProgress questProgress; // QuestProgress referansý

    public int currentQuestIndex;

    private void Awake()
    {
        currentQuestIndex = 0;
        LoadNextQuest();

        if (questProgress != null)
        {
            questProgress.OnQuestComplete.AddListener(CompleteCurrentQuest);
        }
        else
        {
            Debug.LogError("QuestProgress object not found in the scene!");
        }
    }

    private void OnDestroy()
    {
        if (questProgress != null)
        {
            questProgress.OnQuestComplete.RemoveListener(CompleteCurrentQuest);
        }
    }

    private void CompleteCurrentQuest(string questName)
    {
        if (currentQuestIndex < quests.Count)
        {
            // Mevcut görevi tamamla
            Debug.Log("Quest completed: " + quests[currentQuestIndex].questName);

            currentQuestIndex++;
            LoadNextQuest();
        }
       
    }

    private void LoadNextQuest()
    {
        if (currentQuestIndex < quests.Count)
        {
            // Mevcut görevin baþlatýlmasý için gerekli kodlar burada yer alacak
            Debug.Log("Current Quest: " + quests[currentQuestIndex].questName);
        }
        else
        {
            // Tüm görevler tamamlandý
            Debug.Log("All quests completed!");
        }
    }
}