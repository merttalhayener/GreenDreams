using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static TMPro.Examples.TMP_ExampleScript_01;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> quests; // Quest listesini Inspector'dan doldurun

    public int currentQuestIndex;

    public UnityEvent<string> OnQuestComplete;

    public BuildingManager buildingManager;

    [SerializeField]private GameObject goNextButton;
    [SerializeField] private GameObject questProgressButton;


    private void Awake()
    {
        ResetQuests();

        // Diðer baþlangýç kodlarý
        currentQuestIndex = 0;
        LoadNextQuest();

        if (OnQuestComplete == null)
        {
            OnQuestComplete = new UnityEvent<string>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (OnQuestComplete != null)
            {
                CompleteCurrentQuest(currentQuestIndex);
            }
        }

        QuestData currentQuest = GetCurrentQuest();
        if (currentQuest.questType != QuestType.Talk)
        {
            goNextButton.gameObject.SetActive(false);
            questProgressButton.gameObject.SetActive(true);
        }
        else
        {
            goNextButton.gameObject.SetActive(true);
            questProgressButton.gameObject.SetActive(false);
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
        OnQuestComplete.RemoveAllListeners();
    }

    public void CompleteCurrentQuest(int questIndex)
    {
        if (questIndex >= 0 && questIndex < quests.Count)
        {
            QuestData currentQuest = quests[questIndex];

            currentQuest.currentCount++;
            Debug.Log("Quest progress updated: " + currentQuest.questName + " (" + currentQuest.currentCount + "/" + currentQuest.targetCount + ")");

            // Görev tamamlandýysa ilgili iþlemleri gerçekleþtir
            if (currentQuest.currentCount >= currentQuest.targetCount)
            {
                currentQuest.isCompleted = true;
                Debug.Log("Quest completed: " + currentQuest.questName);

                currentQuestIndex++;
                LoadNextQuest();

                 // Mevcut görev tamamlandýktan sonra HandleQuestType çaðrýlýyor
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
                // Görev tamamlandýysa ilgili iþlemleri gerçekleþtir
                Debug.Log("Quest completed: " + currentQuest.questName);
                currentQuestIndex++;
                LoadNextQuest();

                HandleQuestType(); // Mevcut görev tamamlandýktan sonra HandleQuestType çaðrýlýyor
            }
            else
            {
                // Mevcut görevin baþlatýlmasý için gerekli kodlar burada yer alacak
                Debug.Log("Current Quest: " + currentQuest.questName);
            }
        }
        else
        {
            // Tüm görevler tamamlandý
            Debug.Log("All quests completed!");
        }
    }

    public void HandleQuestType()
    {
        QuestData currentQuest = GetCurrentQuest();

        if (currentQuest != null)
        {
            switch (currentQuest.questType)
            {
                case QuestType.Build:
                    HandleBuildQuest();
                    break;
                case QuestType.Collect:
                    HandleCollectQuest();
                    break;
                case QuestType.UseItem:
                    HandleUseItemQuest();
                    break;
                case QuestType.Talk:
                    HandleTalkQuest();
                    break;
                default:
                    Debug.LogError("Unhandled quest type: " + currentQuest.questType);
                    break;
            }
        }
    }

    public void HandleBuildQuest()
    {
        BuildingType objectType = buildingManager.pendingObject.GetComponent<ObjectTypeManager>().buildingType;
        if (quests[currentQuestIndex].wantedBuildingType == objectType)
        {
            CompleteCurrentQuest(currentQuestIndex);
        }
    }
    public void HandleFarmQuest()
    {
        if (quests[currentQuestIndex].questType == QuestType.Farm)
        {
            CompleteCurrentQuest(currentQuestIndex);
        }
    }

    public void HandleCollectQuest()
    {
        // Collect quest özel iþlemlerini burada gerçekleþtirin
    }

    public void HandleUseItemQuest()
    {
        // Use item quest özel iþlemlerini burada gerçekleþtirin
    }

    public void HandleTalkQuest()
    {
        if (quests[currentQuestIndex].questType == QuestType.Talk )
        {
            CompleteCurrentQuest(currentQuestIndex);
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