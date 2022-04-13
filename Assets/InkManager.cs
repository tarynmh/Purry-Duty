using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;


// **** This script acts as our dialogue controller.
// * It takes input (JSON and text fields), and displays the story to the view/scene

public class InkManager : MonoBehaviour
{

    // note: referenced https://klaudiabronowicka.com/blog/2020-12-01-making-a-visual-novel-with-unity-2-5-integration-with-ink/
   [SerializeField]
    private TextAsset
    _inkJsonAsset;
    private Story _story;

    [SerializeField]
    private TextMeshProUGUI _textField;

    [SerializeField]
    private HorizontalLayoutGroup _choiceButtonContainer;

    [SerializeField]
    private Button _choiceButtonPrefab;


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
            text = text?.Trim(); // removes white space from text
            _textField.text = text; // display new text
            Debug.Log("continue");
        }

        else if(_story.currentChoices.Count > 0) // if we have choices
        {
            Debug.Log("choices");
            DisplayChoices();
        }
        else if (!_story.canContinue)
        {
            return;
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
        var choiceButton = Instantiate(_choiceButtonPrefab);
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
}
