using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UserModelScriptNS;

namespace PlayerNameNS {
    public class PlayerName : MonoBehaviour
    {

        // private UserModelScript user; 
        public string nameOfPlayer;
        public string saveName;

        public TextMeshProUGUI inputText;
        public TextMeshProUGUI loadedName;

        public Canvas canvas;

        public bool nameSet;
        
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // nameOfPlayer = user.getName();
            nameOfPlayer = PlayerPrefs.GetString("name", "none");
            loadedName.text = nameOfPlayer;
            if(nameSet && canvas)
            {
                Destroy(canvas.gameObject);
                return;

            }
        }

        public void setName()
        {
            saveName = inputText.text;
            // user.setName(saveName);
            PlayerPrefs.SetString("name", saveName);
            nameSet = true;
        }
    }
}
