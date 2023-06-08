
using UnityEngine;

public class DialogBoxManager : MonoBehaviour
{
    
 
    public void CompleteDialog()
    {
        // Yeni eklenen kod satýrý - QuestManager'ý bul ve HandleObjectPlaced metodunu çaðýr
        QuestManager questManager = FindObjectOfType<QuestManager>();
        if (questManager != null)
        {
            questManager.HandleTalkQuest();
        }
    }
}
