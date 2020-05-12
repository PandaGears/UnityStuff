using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void MainScene()
    {
        SceneManager.LoadScene("Main Scene");
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu Scene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
