using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetTheGame();
        }
    }


    public void ResetTheGame()
    {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Oyun Resetlendi");
        
       
    }
}
