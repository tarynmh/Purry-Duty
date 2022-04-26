using System;
using ItemNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UserStateNS;

namespace UserModelScriptNS {
    [Serializable]
    public class SingleUserModelScript : MonoBehaviour
    {
        [SerializeField]
        private string name;
        private double kibble;
        private string status;
        private List<Item> items;
        private int numCatnip;
        private bool hasHat;
        private int level;
        public static SingleUserModelScript userModelInstance = new SingleUserModelScript(); // greeedy instantiation

        private SingleUserModelScript() {
            name = "";
            kibble = 0.0;
            status = "Happy";
            items = new List<Item>();
            numCatnip = 0;
            hasHat = false;
            level = 0;
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

        public static SingleUserModelScript getInstance() {
            return userModelInstance;
        }

        // Memento Functions
        public UserState getUserState() {
            // return new UserState(name, kibble, status, numCatnip, hasHat, level);
            return new UserState(this);
        }
        public void restoreState(UserState u) {
            // restore the state
            name = u.getName();
            kibble = u.getKibble();
            status = u.getStatus();
            numCatnip = u.getNumCatnip();
            hasHat = u.getHat();
            level = u.getLevel();
        }

        // getters and setters
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

        public void setNumCatnip(int c) {
            numCatnip = c;
        }

        public int getNumCatnip() {
            return numCatnip;
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

        public void setLevel(int l) {
            level = l;
        }

        public void addLevel() {
            level = level + 1;
        }

        public int getLevel() {
            return level;
        }
    }
}