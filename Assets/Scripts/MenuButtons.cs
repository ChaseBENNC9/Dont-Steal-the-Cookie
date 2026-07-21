using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class MenuButtons : MonoBehaviour
{
    public GameObject mainMenuParent;
    public GameObject optionsParent;
    public GameObject loadSlots, saveSlots;
    public void PlayGame()
    {
        GameSettings.gameState = GameState.IN_GAME;
        Cursor.visible = false;
        SceneManager.LoadScene("House");

    }
    public void LoadGame()
    {
        loadSlots.SetActive(true);
    }

    public void NewGame()
    {
        saveSlots.SetActive(true);
    }
    public void PreviousScreen(InputAction.CallbackContext value)
    {
        optionsParent.SetActive(false);

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
        optionsParent.SetActive(true);
    }
    public void MainMenu()
    {
        ES3AutoSaveMgr.Current.Save();
        SceneManager.LoadScene("Menu");
    }

        IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Options",LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


}