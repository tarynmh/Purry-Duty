using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemNS {
    public class Item : MonoBehaviour
    {
        private string name; 
        private string effect;
        private double price; 
        private string description; 
        private Sprite itemImage; // idk if image is the best decision but


        public Item(string n, string e, double p, string d) {
            this.name = n;
            this.effect = e;
            this.price = p;
            this.description = d;
        }

        public void setName(string n) {
            name = n;
        }

        public string getName() {
            return name;
        }

        public void setEffect(string e) {
            effect = e;
        }

        public string getEffect() {
            return effect;
        }

        public void setPrice(double p) {
            price = p;
        }

        public double getPrice() {
            return price;
        }

        public void setDescription(string d) {
            description = d;
        }
        public string getDescription() {
            return description;
        }

        public void setImage(Sprite i) {
            itemImage = i;
        }
        public Sprite getImage() {
            return itemImage;
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