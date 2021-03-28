using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CheckListManager : MonoBehaviour
{
    public Transform content;
    public GameObject addPanel;
    public Button createButton;
    public GameObject checklistItemPrefab;

    private string filepath;

    private List<ChecklistObject> ChecklistObjects = new List<ChecklistObject>();
    private InputField[] addInputFields;

    public class CheckListItem
    {
        public string objName;
        public string type;
        public int index;

        public CheckListItem(string name, string type, int index)
        {
            this.objName = name;
            this.type = type;
            this.index = index;
        }
    }

    private void Start()
    {
        filepath = Application.persistentDataPath + "/checklist.text";
        LoadJsonData();
        addInputFields = addPanel.GetComponentsInChildren<InputField>();

        createButton.onClick.AddListener(delegate
        {
            CreateCheckListItem(addInputFields[0].text, addInputFields[1].text);
        });
    }

    public void SwitchMode(int mode)
    {
        switch (mode)
        {
            //Regular checklistMode
            case 0:
                addPanel.SetActive(false);
                break;
            // Adding a new checklist item
            case 1:
                addPanel.SetActive(true);
                break;
        }
    }

    void CreateCheckListItem(string name, string type, int loadIndex = 0, bool loading = false)
    {
        GameObject item = Instantiate(checklistItemPrefab);
        item.transform.SetParent(content);
        ChecklistObject itemObject = item.GetComponent<ChecklistObject>();
        int index = loadIndex;
        if (!loading) index = ChecklistObjects.Count;
        itemObject.SetObjectInfo(name, type, index);
        ChecklistObjects.Add(itemObject);
        ChecklistObject temp = itemObject;
        itemObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { CheckItem(temp); });

        if (!loading)
        {
            SaveJsonData();
            SwitchMode(0);
        }
    }

    void CheckItem(ChecklistObject item)
    {
        ChecklistObjects.Remove(item);
        SaveJsonData();
        Destroy(item.gameObject);
    }

    void SaveJsonData()
    {
        string contents = "";

        for (int i = 0; i < ChecklistObjects.Count; i++)
        {
            CheckListItem temp = new CheckListItem(ChecklistObjects[i].objName, ChecklistObjects[i].type,
                ChecklistObjects[i].index);
            contents += JsonUtility.ToJson(temp) + "\n";
        }

        File.WriteAllText(filepath, contents);
    }

    void LoadJsonData()
    {
        if (File.Exists(filepath))
        {
            string contents = File.ReadAllText(filepath);

            string[] splitContents = contents.Split('\n');

            foreach (string content in splitContents)
            {
                if (content.Trim() != "")
                {
                    CheckListItem temp = JsonUtility.FromJson<CheckListItem>(content.Trim());
                    CreateCheckListItem(temp.objName, temp.type, temp.index, true);
                }
            }
        }
        else
        {
            Debug.Log("No File!");
        }
    }
}