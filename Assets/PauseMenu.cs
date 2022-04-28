using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UserModelScriptNS;

public class PauseMenu : MonoBehaviour
{

    //NOTE: referenced https://www.youtube.com/watch?v=JivuXdrIHK0
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    [SerializeField]
    private GameObject saveConfirmation;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        saveConfirmation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Debug.Log("esc key pressed");
        //     if(isPaused)
        //     {
        //         Resume();
        //     }
        //     else
        //     {
        //         Pause();
        //     }
        // }
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            pauseMenuUI.SetActive(true);
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SaveGame() 
    {
        Debug.Log("Saving Game...");
        saveConfirmation.SetActive(true);
        Invoke("setFalse", 1f); // sets false/hides after 2 seconds
        SingleUserModelScript.userModelInstance.savePlayerData();
    }

    void setFalse()
    {
        saveConfirmation.SetActive(false); // hides confirmation
    }
}
