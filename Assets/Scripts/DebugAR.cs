using UnityEngine;

public class DebugAR : MonoBehaviour
{
    public GameObject debugPanel;
    private bool debugMode = false;

    public void toggleDebugMode(){
        debugMode = !debugMode;
        debugPanel.SetActive(debugMode);
    }
}
