using UnityEngine;
using UnityEngine.SceneManagement;
public class FlowManager : MonoBehaviour
{
    public void StartGame()
    {
        NextScene();
    }

    public void NextScene()
    {
        // Load Next Scene
        LoadSceneRelative(+1);
    }

    public void PrevScene()
    {
        // Load Prev Scene
        LoadSceneRelative(-1);
    }

    void LoadSceneRelative(int index)
    {
        try
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void Quit()
    {
        Debug.Log("ByeBye");
        Application.Quit();
    }
}
