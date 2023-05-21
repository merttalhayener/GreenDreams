using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> quests; // Quest listesini Inspector'dan doldurun
    public QuestProgress questProgress; // QuestProgress referans�

    public int currentQuestIndex;

    private void Awake()
    {
        ResetQuests();

        // Di�er ba�lang�� kodlar�
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
    private void ResetQuests()
    {
        foreach (QuestData quest in quests)
        {
            quest.currentCount = 0;
            quest.isCompleted = false;
        }
    }

    private void OnDestroy()
    {
        if (questProgress != null)
        {
            questProgress.OnQuestComplete.RemoveListener(CompleteCurrentQuest);
        }
    }

    public void CompleteCurrentQuest(string questName)
    {
        if (currentQuestIndex < quests.Count)
        {
            // Mevcut g�revin ilerlemesini g�ncelle
            QuestData currentQuest = quests[currentQuestIndex];
            if (currentQuest.questName == questName)
            {
                currentQuest.currentCount++;
                Debug.Log("Quest progress updated: " + currentQuest.questName + " (" + currentQuest.currentCount + "/" + currentQuest.targetCount + ")");

                // G�rev tamamland�ysa ilgili i�lemleri ger�ekle�tir
                if (currentQuest.currentCount >= currentQuest.targetCount)
                {
                    currentQuest.isCompleted = true;
                    Debug.Log("Quest completed: " + currentQuest.questName);

                    currentQuestIndex++;
                    LoadNextQuest();
                }
            }
        }
    }

    public void LoadNextQuest()
    {
        if (currentQuestIndex < quests.Count)
        {
            QuestData currentQuest = quests[currentQuestIndex];

            if (currentQuest.isCompleted)
            {
                // G�rev tamamland�ysa ilgili i�lemleri ger�ekle�tir
                Debug.Log("Quest completed: " + currentQuest.questName);
                currentQuestIndex++;
                LoadNextQuest();
            }
            else
            {
                // Mevcut g�revin ba�lat�lmas� i�in gerekli kodlar burada yer alacak
                Debug.Log("Current Quest: " + currentQuest.questName);
            }
        }
        else
        {
            // T�m g�revler tamamland�
            Debug.Log("All quests completed!");
        }
    }

    public void HandleObjectPlaced(GameObject placedObject)
    {
        BuildingType objectType = placedObject.GetComponent<ObjectTypeManager>().buildingType; // ObjectType bile�eninden nesnenin t�r�n� al�n

        foreach (QuestData quest in quests)
        {
            if (quest.wantedBuildingType == objectType) // Nesnenin t�r�n� g�revin t�r�yle k�yaslay�n
            {
                //Debug.Log("Quest Type: " + quest.wantedBuildingType + ", Placed Object Type: " + objectType);
                CompleteCurrentQuest(quest.questName);
                break;
            }
        }
    }

    public QuestData GetCurrentQuest()
    {
        if (currentQuestIndex < quests.Count)
        {
            return quests[currentQuestIndex];
        }
        else
        {
            return null;
        }
    }
}