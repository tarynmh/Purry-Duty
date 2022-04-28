using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ItemNS;


public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantityTxt;
    public TextMeshProUGUI DescTxt;
    public GameObject ShopManager;

    void Start()
    {
        Debug.Log("Start " + ItemID + " - " + ShopManager.GetComponent<ShopManagerScript>().shopItems[ItemID].getPrice());

        PriceTxt.text = "Price: " + ShopManager.GetComponent<ShopManagerScript>().shopItems[ItemID].getPrice().ToString();
        QuantityTxt.text = "Quantity: " + ShopManager.GetComponent<ShopManagerScript>().shopItems[ItemID].getQuantity().ToString();
        DescTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[ItemID].getDescription();
    }
}
