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
}
