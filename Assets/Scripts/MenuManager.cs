using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
        public void PreviousScreen()
    {
        if (MenuInfo.Instance.previousScene !="NONE")
            SceneManager.LoadScene(MenuInfo.Instance.previousScene);
        else
        {
        }
    }
}