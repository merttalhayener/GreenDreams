using UnityEngine.Events;
using UnityEngine;

public class QuestProgressEvent : UnityEvent<string> { }

public class QuestProgress : MonoBehaviour
{
    public UnityEvent<string> OnQuestComplete;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (OnQuestComplete != null)
            {
               
                CompleteQuest("CurrentQuest"); // CompleteQuest methodunu çaðýrarak görevi tamamlayýn
            }
        }
    }

    private void CompleteQuest(string questName)
    {
        // Quest tamamlandýðýnda eventi tetikle
        if (OnQuestComplete != null)
        {
            
            OnQuestComplete.Invoke(questName);
        }
    }

}