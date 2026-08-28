using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class Tab
{
    public GameObject tabPanel;
    public Button tabButton;
    public Color activeColor, inactiveColor;


    public void Init()
    {
        tabButton.GetComponentInChildren<TextMeshProUGUI>().text = tabButton.gameObject.name;
        tabPanel.name = tabButton.gameObject.name;
        CloseTab();
    }

    public void OpenTab()
    {
        tabButton.GetComponent<Image>().color = activeColor;
        tabPanel.SetActive(true);
        tabButton.transform.Find("highlight").gameObject.SetActive(true);

    }

    public void CloseTab()
    {
        tabButton.GetComponent<Image>().color = inactiveColor;
        tabButton.transform.Find("highlight").gameObject.SetActive(false);
        tabPanel.SetActive(false);
    }
}
public class TabbedView : MonoBehaviour
{
    public List<Tab> tabs;
    public Tab activeTab;
    private int activeTabIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(tabs.Count < 1) 
            Debug.LogError("Cannot contain 0 tabs");
        else
        {
            activeTabIndex = 0;
            activeTab = tabs[activeTabIndex];
            foreach(Tab tab in tabs)
            {
            tab.Init();
            tab.tabButton.onClick.AddListener(() => OpenTab(tab));
            }
            activeTab.OpenTab();
        }
    }

    private void OpenTab(Tab tab)
    {
        activeTab.CloseTab();
        activeTab = tab;
        activeTab.OpenTab();
        activeTabIndex = tabs.IndexOf(tab);

    }

    public void NextTab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(activeTabIndex == tabs.Count - 1)
            {
                activeTabIndex = 0;
            }
            else
            {
                activeTabIndex++;
            }
            OpenTab(tabs[activeTabIndex]);
        }
    }
    public void PreviousTab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(activeTabIndex == 0)
            {
                activeTabIndex = tabs.Count-1;
            }
            else
            {
                activeTabIndex--;
            }
            OpenTab(tabs[activeTabIndex]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
