using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class userNameTag : MonoBehaviour
{

    public TextMeshProUGUI loadedName;
    // Start is called before the first frame update
    void Start()
    {
        loadedName.text = PlayerPrefs.GetString("name", " ");
    }

    // Update is called once per frame
    void Update()
    {
        // nameOfPlayer = PlayerPrefs.GetString("name", " ");
        
    }
}
