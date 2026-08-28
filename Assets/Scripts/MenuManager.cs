using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public GameObject mainMenuParent,optionsParent,loadSlots, saveSlots,characterSelection;

    private EventSystem currentEventSystem;
    public EventSystem CurrentEventSystem  {  get => currentEventSystem;}
    private GameObject currentlySelected;
    private Stack<GameObject> menuPanelStack;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
        menuPanelStack = new Stack<GameObject>();
        currentEventSystem = EventSystem.current;
        currentlySelected = currentEventSystem.currentSelectedGameObject;
    }

    public void AddToMenuStack(GameObject panel)
    {
        if (menuPanelStack.Contains(panel)) return;
        menuPanelStack.Push(panel);
        panel.SetActive(true);
        if (panel.TryGetComponent<MenuPanel>(out MenuPanel menuPanel)){
            if(menuPanel.firstSelected != null)
            {
                if (menuPanel.firstSelected.TryGetComponent<Selectable>(out Selectable selectable))
                {
                    selectable.Select();
                }
            }
        }
    }
    public void PopMenuStack()
    {
        Debug.Log("POP");
        if(menuPanelStack.Count < 1) return;
        GameObject panel = menuPanelStack.Pop();
        Debug.Log("POPPED" + panel);
        panel.SetActive(false);
    }


    private void Update()
    {
        //Check if the last known selected GameObject has changed since
        //the last frame
        if (currentEventSystem.currentSelectedGameObject != null &&
            currentlySelected != currentEventSystem.currentSelectedGameObject)
        {
            currentlySelected = currentEventSystem.currentSelectedGameObject;
        }

        // The currentSelectedGameObject will be null when you click with your
        // anywhere on the screen on a non-Selectable GameObject.
        if (currentEventSystem.currentSelectedGameObject == null)
        {
            // If this happens simply re-select the last known selected GameObject.
            if (currentlySelected != null)
            {
                currentlySelected.GetComponent<Selectable>().Select();
            }
            else
            {
                // If there is none, select the firstSelectedGameObject
                // (which can be setup inthe EventSystem component).
                currentlySelected = currentEventSystem.firstSelectedGameObject;
                currentlySelected.GetComponent<Selectable>().Select();
            }
        }
    }

}