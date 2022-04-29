using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ink.Runtime;
using TMPro;
using UserModelScriptNS;
using FactoryNS;

// **** This script acts as our dialogue controller.
// * It takes input (JSON and text fields), and displays the story to the view/scene

public class InkManager : MonoBehaviour
{
    // note: referenced https://klaudiabronowicka.com/blog/2020-12-01-making-a-visual-novel-with-unity-2-5-integration-with-ink/
   [SerializeField]
    private TextAsset
    _inkJsonAsset;

    // 2 different json files will be loaded in depending on the user's mood
    [SerializeField]
    private TextAsset
    levelAssetHappy;

    [SerializeField]
    private TextAsset
    levelAssetSad;

    private Story _story;

    [SerializeField]
    private TextMeshProUGUI _textField;

    [SerializeField]
    private HorizontalLayoutGroup _choiceButtonContainer;

    [SerializeField]
    private Button _choiceButtonPrefab;

    [SerializeField]
    private BtnFactory factory;

    private SingleUserModelScript userModel = SingleUserModelScript.userModelInstance;

    private List<string> tags;

    void Start()
    {
        StartStory();
    }

    // used to make the story
    public void StartStory()
    {
        _story = new Story(_inkJsonAsset.text);
        DisplayNextLine();
    }

    // display lines of our story
    public void DisplayNextLine()
    {   
        if (_story.canContinue)
        {
            string text = _story.Continue(); // get next line
            parseTags();
            text = text?.Trim(); // removes white space from text
            _textField.text = text; // display new text
            Debug.Log("continue");
            Debug.Log(_story.canContinue);
        }

        else if(_story.currentChoices.Count > 0) // if we have choices
        {
            Debug.Log("choices");
            DisplayChoices();
            parseTags();
        }
        else if (!_story.canContinue)
        {
            Debug.Log("cant continue for some reason");
            Debug.Log("Level" + SingleUserModelScript.userModelInstance.getLevel().ToString());
            SingleUserModelScript.userModelInstance.addLevel();
            // UnityEngine.SceneManagement.SceneManager.LoadScene(("Level"+SingleUserModelScript.userModelInstance.getLevel().ToString()), LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // check if there's more to the story
            DisplayNextLine();
        }
    }

    private void DisplayChoices()
    {
        // checks if choices are already being displayed
        // ERROR
        if(_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0)
        {
            Debug.Log(_story.currentChoices);
            Debug.Log("going to return, choices 'are displayed'");
            return;
        }

        for(int i=0; i < _story.currentChoices.Count; i++) // iterate through choices
        {
            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text); // creates choice button

            button.onClick.AddListener(() => OnClickChoiceButton(choice));

            Debug.Log("in loop");
        }
    }

    public Button CreateChoiceButton(string text)
    {
        Debug.Log("creating button");
        // create button from prefab we made
        // var choiceButton = Instantiate(_choiceButtonPrefab);
        var choiceButton = factory.GetNewInstance();
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);
        
        // sets text
        var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;

        return choiceButton;
    }

    public void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // lets ink know which choice was selected
        RefreshChoiceView(); // remove the choices
        DisplayNextLine(); // displays next line
    }

    public void RefreshChoiceView()
    {
        if (_choiceButtonContainer != null)
        {
            foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject); //destroys each button and associated objects in container
            }
        }
    }

    public void parseTags()
    {
        if(_story.currentTags.Count > 0)
        {
            tags = _story.currentTags;
            Debug.Log("sWITCH CASE");

            foreach(string tag in tags)
            {
                Debug.Log("this is in foreach");
                Debug.Log(tag);
                string tagType = tag.Split(' ')[0]; // gets the method that we want to perform
                string tagAction = tag.Split(' ')[1]; // gets specifics of the action we want
                switch(tagType) 
                {
                    case "changeMood":
                        SingleUserModelScript.userModelInstance.setStatus(tagAction);
                        Debug.Log("IN CASE");
                        Debug.Log(SingleUserModelScript.userModelInstance.getStatus());
                        break;
                }
            }
        }
    }
}
