using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UserModelScriptNS;
using TMPro;

[System.Serializable]
public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[4, 4];
    public TextMeshProUGUI kibbleTXT;
    public int kibble;

    void Awake()
    {
        kibbleTXT.text = "Kibble: " + SingleUserModelScript.userModelInstance.getKibble().ToString();

        //shop item IDs
        shopItems[1, 1] = 1; // catnip
        shopItems[1, 2] = 2; // tuna
        shopItems[1, 3] = 3; // hat

        // Price
        shopItems[2, 1] = 300; // catnip
        shopItems[2, 2] = 100; // tuna
        shopItems[2, 3] = 500; // hat

        // Quantity (also update in the user model)
        shopItems[3, 1] = 0; // catnip
        shopItems[3, 2] = 0; // tuna
        shopItems[3, 3] = 0; // hat

        Debug.Log("Init");
    }

    public void Buy()
    { 
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        Debug.Log("Buy ID: " + ButtonRef.GetComponent<ButtonInfo>().ItemID);
        if(SingleUserModelScript.userModelInstance.getKibble() >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            Debug.Log("Can buy");
            if(ButtonRef.GetComponent<ButtonInfo>().ItemID == 3) { // if hat -- can only buy 1 hat
                if(shopItems[3,3] < 0) { // if already have hat

                }
                else {
                    SingleUserModelScript.userModelInstance.setHat(true);
                    SingleUserModelScript.userModelInstance.removeKibble(shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]);
                    shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++; // update quantity in shop manager and usermodel:
                }
            }
            else { // if food
                SingleUserModelScript.userModelInstance.removeKibble(shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]);
                shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++; // update quantity in shop manager and usermodel:
                
                if(ButtonRef.GetComponent<ButtonInfo>().ItemID == 1) { // if catnip
                    SingleUserModelScript.userModelInstance.addCatnip();
                }
                else if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 2) { // if tuna
                    SingleUserModelScript.userModelInstance.addTuna();
                }
            }

            kibbleTXT.text = "Kibble: " + SingleUserModelScript.userModelInstance.getKibble().ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = "Quantity: " + shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

        }
    }

    public void NextLevel() {
        SingleUserModelScript.userModelInstance.addLevel(); // update level
        // go to next level
        UnityEngine.SceneManagement.SceneManager.LoadScene(("Level"+SingleUserModelScript.userModelInstance.getLevel().ToString()), LoadSceneMode.Additive);
        // return;
    }
}
