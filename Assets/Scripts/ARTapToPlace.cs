using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject diamondObjectToPlace;
    [SerializeField] private GameObject starObjectToPlace;
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private DatabaseHandlerForString dataBase;
    private bool placementPoseIsValid = false;
    private Pose placementPose;
    private int indexOfObjectToPlace = 0;

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if(placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        switch (indexOfObjectToPlace)
        {
            case 0:
                Instantiate(diamondObjectToPlace, placementPose.position, placementPose.rotation);
                dataBase.PostLocation("Diamond");
                break;
            case 1:
                Instantiate(starObjectToPlace, placementPose.position, placementPose.rotation);
                dataBase.PostLocation("Star");
                break;
            default:
                Debug.Log("Error placing object");
                break;
        }
        placementIndicator.SetActive(false);
        gameObject.SetActive(false);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }

        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    public void GetIndexOfObject(int index)
    {
        indexOfObjectToPlace = index;
    }
}