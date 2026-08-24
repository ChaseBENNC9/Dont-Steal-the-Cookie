using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(tabs.Count < 1) 
            Debug.LogError("Cannot contain 0 tabs");
        else
        {
            activeTab = tabs[0];
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

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
