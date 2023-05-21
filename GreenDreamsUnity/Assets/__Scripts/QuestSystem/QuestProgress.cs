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
               
                CompleteQuest("CurrentQuest"); // CompleteQuest methodunu �a��rarak g�revi tamamlay�n
            }
        }
    }

    private void CompleteQuest(string questName)
    {
        // Quest tamamland���nda eventi tetikle
        if (OnQuestComplete != null)
        {
            
            OnQuestComplete.Invoke(questName);
        }
    }

}