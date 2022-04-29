using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UserModelScriptNS;
using ItemNS;
using TMPro;

[System.Serializable]
public class ShopManagerScript : MonoBehaviour
{
    // inspired by: https://www.youtube.com/watch?v=Oie-G5xuQNA 
    public Item[] shopItems = new Item[4]; // -- indexed by id
    public TextMeshProUGUI kibbleTXT;
    public int kibble;

    void Awake()
    {
        kibbleTXT.text = "Kibble: " + SingleUserModelScript.userModelInstance.getKibble().ToString();

        shopItems[1] = new Item("catnip", 1, 300.0, "Catnip helps make a bad day better! Use when you need a pick-me-up!", 0); // catnip
        shopItems[2] = new Item("tuna", 2, 100.0, "A light snack for a long day. Some tuna can help brighten the mood!", 0); // tuna
        shopItems[3] = new Item("hat", 3, 500.0, "A snazzy hat for a snazzy cat. Look cute and jury at the same time!", 0); // hat

        Debug.Log("Init");
    }

    public void Buy()
    { 
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int givenID = ButtonRef.GetComponent<ShopButtonInfo>().ItemID;
        Debug.Log("Buy ID: " + givenID);
        if(SingleUserModelScript.userModelInstance.getKibble() >= shopItems[givenID].getPrice())
        {
            Debug.Log("Can buy");
            if(ButtonRef.GetComponent<ShopButtonInfo>().ItemID == 3) { // if hat -- can only buy 1 hat
                if(shopItems[3].getQuantity() != 0) { // if already have hat
                    // update description, explains why cant buy a hat again
                    shopItems[3].setDescription("Already purchased! Looking snazzy for the next day at court!");
                    ButtonRef.GetComponent<ShopButtonInfo>().DescTxt.text = shopItems[givenID].getDescription();
                    Debug.Log("User Hat: " + SingleUserModelScript.userModelInstance.getHat());
                }
                else {
                    Debug.Log("Bought hat"); // update quantity in shop manager and kibble in usermodel:
                    SingleUserModelScript.userModelInstance.setHat(true);
                    SingleUserModelScript.userModelInstance.removeKibble(shopItems[givenID].getPrice());
                    shopItems[3].addQuantity(); 
                    // update description, explains why cant buy a hat again
                    shopItems[3].setDescription("Purchased! Looking snazzy for the next day at court!");
                    ButtonRef.GetComponent<ShopButtonInfo>().DescTxt.text = shopItems[givenID].getDescription();
                }
            }
            else { // if food
                SingleUserModelScript.userModelInstance.removeKibble(shopItems[givenID].getPrice());
                shopItems[givenID].addQuantity(); // update quantity in shop manager and usermodel:
                
                if(ButtonRef.GetComponent<ShopButtonInfo>().ItemID == 1) { // if catnip
                    SingleUserModelScript.userModelInstance.addCatnip();
                }
                else if (ButtonRef.GetComponent<ShopButtonInfo>().ItemID == 2) { // if tuna
                    SingleUserModelScript.userModelInstance.addTuna();
                }
            }

            kibbleTXT.text = "Kibble: " + SingleUserModelScript.userModelInstance.getKibble().ToString();
            ButtonRef.GetComponent<ShopButtonInfo>().QuantityTxt.text = "Quantity: " + shopItems[givenID].getQuantity().ToString();

        }

    }

    public void NextLevel() {
        SingleUserModelScript.userModelInstance.addLevel(); // update level
        Debug.Log("Level: " + SingleUserModelScript.userModelInstance.getLevel());
        // go to next level
        UnityEngine.SceneManagement.SceneManager.LoadScene(("Level"+SingleUserModelScript.userModelInstance.getLevel().ToString()));
        // return;
    }
}
