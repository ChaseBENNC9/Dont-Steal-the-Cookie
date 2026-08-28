using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class CustomDropdown : MonoBehaviour
{
    public List<string> options;
    public List<GameObject> optionObjects;
    private Button button;
    private Image image;
    public Sprite defaultSprite;
    public Sprite dropDownSprite;
    public Sprite activeSprite;
    public bool active;
    public GameObject dropdownBox;
    public GameObject item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        active = false;
        SetSprite(active);
        optionObjects = new List<GameObject>();
        button.onClick.AddListener(() => Toggle() );
        LoadItems();
    }
    private void LoadItems()
    {
        foreach (string label in options)
        {
            GameObject option = Instantiate(item,item.transform.parent);
            option.transform.Find("Item Label").GetComponent<TextMeshProUGUI>().text = label;
            option.name = label;
            option.SetActive(true);
            optionObjects.Add(option);
            option.GetComponent<Toggle>().isOn = false;
        }
    }

    public void SelectItem(Toggle item)
    {
        int index = optionObjects.IndexOf(item.gameObject);
        if (item.isOn)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = options[index];
            for (int i = 0; i < options.Count ; i++)
            {
                if (i!=index)
                    optionObjects[i].GetComponent<Toggle>().isOn = false;
            }
            Toggle();
            
        }
        
    }
    private void Toggle()
    {
        if (active)
        {
            active = false;
        }
        else
        {
            active = true;
        }
        SetSprite(active);
    }
    private void SetSprite(bool b)
    {
        if (b)
        {
            image.sprite = activeSprite;

        }
        else
        {
            image.sprite = defaultSprite;
        }
        dropdownBox.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
