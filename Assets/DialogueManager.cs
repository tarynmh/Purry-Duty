using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

namespace DialogueManager 
{

    public class DialogueManager : MonoBehaviour
    {

        public TextAsset inkFile;
        public GameObject textBox;
        public GameObject customButton;
        public GameObject optionPanel;

        static Story story;
        
        Text nameTag;
        Text msg;
        List<string> tags;
        static Choice choiceSelected;
        // Start is called before the first frame update
        void Start()
        {
            story = new Story(inkFile.text);
            nameTag = textBox.transform.GetChild(0).GetComponent<Text>();
            msg = textBox.transform.GetChild(1).GetComponent<Text>();
            tags = new List<string>();
            choiceSelected = null;
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // check if there's more to the story
                if(story.canContinue)
                {
                    // TODO: change name to be user input
                    nameTag.text = "Mittens";
                    AdvanceDialogue();

                    // check for choices
                    if(story.currentChoices.Count != 0)
                    {
                        StartCoroutine(ShowChoices());
                    }
                }
                else
                {
                    FinishDialogue();
                }
            }
        }

        public void AdvanceDialogue() {
            string currSentence = story.Continue(); // gets next sentence in file
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currSentence));
        }

        private void FinishDialogue()
        {
            Debug.Log("You've reached the end of the dialogue.");
        }

        // makes the dialogue flow a little nicer and writes it out one-by-one
        IEnumerator TypeSentence(string sentence)
        {
            msg.text = "";
            foreach(char letter in sentence.ToCharArray())
            {
                msg.text += letter;
                yield return null;
            }
        }

        // show choices
        IEnumerator ShowChoices()
        {
            Debug.Log("No choices available :(");
            List<Choice> choices = story.currentChoices;

            for(int i = 0; i < choices.Count; i++)
            {
                GameObject temp = Instantiate(customButton, optionPanel.transform);
                temp.transform.GetChild(0).GetComponent<Text>().text = choices[i].text;
                temp.AddComponent<Selectable>();
                temp.GetComponent<Selectable>().element = choices[i];
                temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
            }

            optionPanel.SetActive(true);

            yield return new WaitUntil(() => { return choiceSelected != null; });

            AdvanceFromDecision();
        }

        // lets the story know which branch to go to
        public static void SetDecision(object elem)
        {
            choiceSelected = (Choice)elem;
            story.ChooseChoiceIndex(choiceSelected.index);
        }

        public void AdvanceFromDecision()
        {
            optionPanel.SetActive(false);
            for (int i = 0; i < optionPanel.transform.childCount; i++)
            {
                Destroy(optionPanel.transform.GetChild(i).gameObject);
            }
            choiceSelected = null; // reset to null
            AdvanceDialogue();
        }

    }
}