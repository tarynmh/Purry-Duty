using System;
using ItemNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UserModelScriptNS {
    [Serializable]
    public class SingleUserModelScript : MonoBehaviour
    {
        [SerializeField]
        private string name;
        private double kibble;
        private string status;
        private List<Item> items;
        private bool hasHat;
        public static SingleUserModelScript userModelInstance = new SingleUserModelScript();
        //TODO: just one text for name

        private SingleUserModelScript() {
            name = "";
            kibble = 0.0;
            status = "Happy";
            items = new List<Item>();
            hasHat = false;
        }

        // public static SingleUserModelScript getInstance() {
        //     if(userModelInstance == null) {
        //         userModelInstance = new SingleUserModelScript();
        //     }
        //     return userModelInstance;
        // }

        // checks to see if there's an instance of the singleton already -- if there's a duplicate, you need to delete it
        private void SingletonCheck() 
        { 
            if (userModelInstance != null && userModelInstance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                userModelInstance = this; 
            } 
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

        public void setHat(bool h) {
            this.hasHat = h;
        }

        public bool getHat() {
            return this.hasHat;
        }

        public void setStatus(string s) {
            this.status = s;
        }

        public string getStatus() {
            return this.status;
        }

        // // Start is called before the first frame update
        // void Start()
        // {
            
            
        // }

        // // Update is called once per frame
        // void Update()
        // {
            
        // }
    }
}