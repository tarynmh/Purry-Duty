using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manager = DialogueManager.DialogueManager;

public class Selectable : MonoBehaviour
{
    public object element;
    public void Decide()
    {
        manager.SetDecision(element);
    }
}
