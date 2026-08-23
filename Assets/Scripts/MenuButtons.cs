using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        GameSettings.gameState = GameState.IN_GAME;
        Cursor.visible = false;
        SceneManager.LoadScene("House");
    }
    public void LoadGame()
    {
        MenuManager.Instance.AddToMenuStack(MenuManager.Instance.loadSlots);
    }

    public void NewGame()
    {
        MenuManager.Instance.AddToMenuStack(MenuManager.Instance.saveSlots);

    }
    public void CharacterSelection()
    {
        MenuManager.Instance.AddToMenuStack(MenuManager.Instance.characterSelection);
    }
    public void PreviousScreen(InputAction.CallbackContext value)
    {
        if( value.performed){
            
        MenuManager.Instance.PopMenuStack();
        }

    }
    public void Quit()
    {
        Application.Quit();
    }

    public void SelectButton(Button button)
    {
        button.Select();
    }

    public void Options()
    {
        MenuManager.Instance.AddToMenuStack(MenuManager.Instance.optionsParent);
    }
    public void MainMenu()
    {
        // ES3AutoSaveMgr.Current.Save();
        SceneManager.LoadScene("Menu");
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Options", LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


}