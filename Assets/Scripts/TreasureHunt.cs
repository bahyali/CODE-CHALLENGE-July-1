using System;
using System.Collections.Generic;
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

    public Camera arCamera;

    private Animator chestAnimation;

    public Text instructions;

    private GameObject deployedChest;

    public GameObject restartBtn;

    void Start()
    {
        instructions.text = "Let's go look for the treasure!";
    }
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

        if (chestVisible && !chestOpened && positionIndicator.placementPoseIsValid)
        {
            try
            {
                Ray ray = new Ray(arCamera.transform.position, arCamera.transform.rotation * Vector3.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, closeness))
                {
                    if (hit.collider.tag == "chest")
                    {
                        openChest();
                    }
                }
            }
            catch (Exception e)
            {
                debug.text = e.ToString();
            }

        }

    }

    private void openChest()
    {
        chestAnimation.SetBool("openChest", true);
        instructions.text = "Confetti! you found the treasure and finished the Demo.";
        restartBtn.SetActive(true);
        chestOpened = true;
    }
    private void showChest()
    {
        if (positionIndicator.placementPoseIsValid)
        {
            chestVisible = true;
            instructions.text = "Now go closer to the treasure!";
            Pose placementPose = positionIndicator.getPlacementPose();
            deployedChest = Instantiate(chest, placementPose.position, chest.transform.rotation);
            chestAnimation = deployedChest.GetComponent<Animator>();
        }
    }
}
