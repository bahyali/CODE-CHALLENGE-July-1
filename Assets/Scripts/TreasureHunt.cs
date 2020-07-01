using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class TreasureHunt : MonoBehaviour
{
    public GameObject chest;

    public Text debug;

    public CountSteps stepCounter;

    public PositionIndicator positionIndicator;

    public float closeness;

    public int stepLimit;

    private int steps;

    private bool chestVisible = false;

    private bool chestOpened = false;

    private Camera arCamera;

    private void Update()
    {
        steps = stepCounter.stepCount;

        // Show chest
        if (!chestVisible && steps >= stepLimit)
            showChest();

        if (!chestVisible && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            showChest();
        }

        if (chestVisible && !chestOpened)
        {
            try{
            // Close to chest
            RaycastHit hit;
            Ray closenessRay = arCamera.ScreenPointToRay(positionIndicator.getPlacementIndicator().transform.position);
            debug.text = Vector3.Distance(arCamera.transform.position, chest.transform.position).ToString();

            if (Physics.Raycast(closenessRay, out hit, closeness))
            {
                debug.text = "OPEN TREASURE!";
                chestOpened = true;
            }
            } catch (Exception e){
                debug.text = e.ToString();
            }
           
        }


    }

    private void showChest()
    {
        if (positionIndicator.placementPoseIsValid)
        {
            chestVisible = true;
            debug.text = "SHOW TREASUREs!";
            Pose placementPose = positionIndicator.getPlacementPose();
            Quaternion rotation = placementPose.rotation;
            debug.text = rotation.ToString();
            Instantiate(chest, placementPose.position, rotation);
        }
        Debug.Log("Show Chest");
    }
}
