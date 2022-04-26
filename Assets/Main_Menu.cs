using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{

    // Menu Controller to change to various views/scenes

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void LoadGame()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        // call memento somehow probably
    }

    public void Quit()
    {
        Application.Quit();
    }
}
