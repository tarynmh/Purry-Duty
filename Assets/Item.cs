using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemNS {
    public class Item : MonoBehaviour
    {
        private string name; 
        private int itemID;
        private double price; 
        private string description; 
        private int quantity;
        // private Sprite itemImage; // idk if image is the best decision but


        public Item(string n, int i, double p, string d, int q) {
            this.name = n;
            this.itemID = i;
            this.price = p;
            this.description = d;
            this.quantity = q;
        }

        public void setName(string n) {
            name = n;
        }

        public string getName() {
            return name;
        }

        public void setID(int i) {
            itemID = i;
        }

        public int getID() {
            return itemID;
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

        public int getQuantity() {
            return quantity;
        }

        public void setQuantity(int q) {
            quantity = q;
        }

        public void addQuantity() {
            quantity = quantity + 1;
        }

    }
}