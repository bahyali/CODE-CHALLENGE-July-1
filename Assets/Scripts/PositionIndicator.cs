using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.XR.ARSubsystems;

public class PositionIndicator : MonoBehaviour
{
    public GameObject placementIndicator;
    public Pose placementPose;
    public bool placementPoseIsValid {get; private set;} = false;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager raycastManager;

    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        updatePlacementPose();
        updatePlacementIndicator();
    }

    private void updatePlacementIndicator()
    {
        placementIndicator.SetActive(placementPoseIsValid);
        placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
    }

    private void updatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if(placementPoseIsValid){
            placementPose  = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public Pose getPlacementPose(){
        return placementPose;
    }

    public GameObject getPlacementIndicator(){
        return placementIndicator;
    }
}
