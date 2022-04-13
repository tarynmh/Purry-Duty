using System;
using ItemNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UserModelScriptNS {
    [Serializable]
    public class UserModelScript : MonoBehaviour
    {
        [SerializeField]
        private string name;
        private double kibble;
        private List<Item> items;
        private Sprite catPicture; 
        private Sprite pictureBonus;
        public TextMeshProUGUI inputText;
        public TextMeshProUGUI loadedName;

        public UserModelScript() {

        }

        public void setName(string n) {
            name = n;
        }

        public string getName() {
            return name;
        }

        public void setKibble(double k) {
            kibble = k;
        }

        public double getKibble() {
            return kibble;
        }

        public void addItem(Item i) {
            items.Add(i);
        }

        public void removeItem(Item i) {
            if(items.Contains(i)) {
                items.Remove(i);
            }
        }

        public void setCatPicture(Sprite i) {
            this.catPicture = i;
        }

        public void setPictureBonus(Sprite i) {
            this.pictureBonus = i;
        }

        public Sprite getCatPicture() {
            return this.catPicture;
        }

        public Sprite getPictureBonus() {
            return this.pictureBonus;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}