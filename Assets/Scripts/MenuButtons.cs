using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Testing");
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
        SceneManager.LoadScene("Options");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}