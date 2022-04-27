using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantityTxt;
    public GameObject ShopManager;

    void Start()
    {
        Debug.Log("Start " + ItemID + " - " + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID]);
        int[,] items = ShopManager.GetComponent<ShopManagerScript>().shopItems;

        PriceTxt.text = "Price: " + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = "Quantity: " + ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}
