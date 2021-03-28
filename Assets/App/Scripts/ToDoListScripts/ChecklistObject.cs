using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistObject : MonoBehaviour
{
    public string objName;
    public string type;
    public int index;

    private Text itemText;

    private void Start()
    {
        itemText = GetComponentInChildren<Text>();
        itemText.text = objName;
        
    }

    public void SetObjectInfo(string name, string type, int index)
    {
        this.objName = name;
        this.type = type;
        this.index = index;
    }

}
