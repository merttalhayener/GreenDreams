using UnityEngine;
using TMPro;

public class QuestPanelUI : MonoBehaviour
{
    public QuestManager questManager;
   // public TMP_Text questNameText;
    public TMP_Text questDescriptionText;
    public TMP_Text questProgressText;

    private void Update()
    {
        UpdateQuestPanel();
    }

    public void UpdateQuestPanel()
    {
        QuestData currentQuest = questManager.GetCurrentQuest();

        if (currentQuest != null)
        {
            //questNameText.text = currentQuest.questName;
            questDescriptionText.text = currentQuest.questDescription;
            questProgressText.text = string.Format("{0}/{1}", currentQuest.currentCount, currentQuest.targetCount);
        }
        else
        {
           // questNameText.text = "";
            questDescriptionText.text = "";
            questProgressText.text = "";
        }
    }
}