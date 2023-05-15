using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    public void ResetTheGame()
    {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Oyun Resetlendi");
        
       
    }
}
