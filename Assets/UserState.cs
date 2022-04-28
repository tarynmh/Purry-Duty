using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemNS;
using UserModelScriptNS;

namespace UserStateNS {
    [System.Serializable]
    public class UserState
    {
        private string name;
        private double kibble;
        private string status;
        private int numCatnip;
        private int numTuna;
        private bool hasHat;
        // need to keep track of level
        private int level;
        
        public UserState(SingleUserModelScript user) {
            name = user.getName();
            kibble = user.getKibble();
            status = user.getStatus();
            numCatnip = user.getNumCatnip();
            numTuna = user.getNumTuna();
            hasHat = user.getHat();
            level = user.getLevel();
        }

        public string getName() {
            return name;
        }

        public double getKibble() {
            return kibble;
        }

        public string getStatus() {
            return status;
        }

        public int getNumCatnip() {
            return numCatnip;
        }

        public int getNumTuna() {
            return numTuna;
        }

        public bool getHat() {
            return hasHat;
        }

        public int getLevel() {
            return level;
        }

    }

}