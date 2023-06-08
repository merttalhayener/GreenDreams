
using UnityEngine;

public class DialogBoxManager : MonoBehaviour
{
    
 
    public void CompleteDialog()
    {
        // Yeni eklenen kod sat�r� - QuestManager'� bul ve HandleObjectPlaced metodunu �a��r
        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null)
        {
            questManager.HandleTalkQuest();
        }
    }
}
