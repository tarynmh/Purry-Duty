using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ink.Runtime;
using TMPro;
using UserModelScriptNS;


// **** This script acts as our dialogue controller.
// * It takes input (JSON and text fields), and displays the story to the view/scene

public class InkManagerLvl : MonoBehaviour
{
    // note: referenced https://klaudiabronowicka.com/blog/2020-12-01-making-a-visual-novel-with-unity-2-5-integration-with-ink/

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
    private SpriteRenderer catRenderer;

    [SerializeField]
    private SpriteRenderer bgRenderer;

    [SerializeField]
    private SpriteRenderer userRenderer;

    [SerializeField]
    private Sprite[] userImgs;

    [SerializeField]
    private Sprite[] cats;  // different cat images

    [SerializeField]
    private Sprite[] backgrounds; // different background images

    private SingleUserModelScript userModel = SingleUserModelScript.userModelInstance;

    private List<string> tags;

    private Factory btnFactory; // used to make button prefabs

    void Start()
    {
        StartStory();
    }

    // used to make the story
    public void StartStory()
    {
        if(SingleUserModelScript.userModelInstance.getStatus() == "happy") {
            _story = new Story(levelAssetHappy.text);
        }
        else {
            _story = new Story(levelAssetSad.text);
        }
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
            // UnityEngine.SceneManagement.SceneManager.LoadScene(("Level"+SingleUserModelScript.userModelInstance.getLevel().ToString()), LoadSceneMode.Additive);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
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
        // var choiceButton = btnFactory.GetNewInstance();
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

                    case "bgChange":
                        if(tagAction == "court") {
                            // set the scaling
                            Vector3 pos = new Vector3(-5f, -4f, -2184f);
                            catRenderer.transform.position = pos;
                            // set the scaling
                            Vector3 scale = new Vector3(34.55f, 28.245f, 18.72f);
                            catRenderer.transform.localScale = scale;
                            bgRenderer.sprite = backgrounds[0]; // change to court
                        }
                        else if (tagAction == "outside") {
                            // set the scaling
                            Vector3 pos = new Vector3(-81.4488f, 0.42f, 0f);
                            catRenderer.transform.position = pos;
                            // set the scaling
                            Vector3 scale = new Vector3(0.7559f, 0.90615f);
                            catRenderer.transform.localScale = scale;
                            bgRenderer.sprite = backgrounds[1];
                        }
                        break;
                    case "catChange":
                        if(tagAction == "judge") {
                            // set the scaling
                            Vector3 pos = new Vector3(-80.71f, 0.06f, 0f);
                            catRenderer.transform.position = pos;
                            // set the scaling
                            Vector3 scale = new Vector3(0.8979f, 0.90615f);
                            catRenderer.transform.localScale = scale;
                            catRenderer.sprite = cats[0]; // change to judge
                        }
                        else if(tagAction == "matches") {
                            // set the scaling
                            Vector3 pos = new Vector3(-80.46f, 0.82f, 0f);
                            catRenderer.transform.position = pos;
                            // set the scaling
                            Vector3 scale = new Vector3(0.71f, 0.72f);
                            catRenderer.transform.localScale = scale;
                            catRenderer.sprite = cats[1]; // change to matches
                        }
                        else if (tagAction == "tiger") {
                            // set the scaling
                            Vector3 pos = new Vector3(-81.45f, 0.42f,0f );
                            catRenderer.transform.position = pos;
                            // set the scaling
                            Vector3 scale = new Vector3(0.76f, 0.91f);
                            catRenderer.transform.localScale = scale;
                            catRenderer.sprite = cats[2]; // change to tiger
                        }
                        else if(tagAction == "none"){
                            catRenderer.sprite = null;
                        }
                        break;
                    case "addKibble":
                        double kibbleToAdd = Convert.ToDouble(tagAction);
                        SingleUserModelScript.userModelInstance.addKibble(kibbleToAdd);
                        break;

                    case "userChange":
                        if(tagAction == "sad")
                        {
                            userRenderer.sprite = userImgs[0]; // change to sad cat
                        }
                        break;
                }

            }
        }
    }
}
