using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LocationService : MonoBehaviour
{
    [SerializeField] private Text latitudeText;
    [SerializeField] private Text longitudeText;

    public float latitude;
    public float longitude;

    private void Start()
    {
        Input.location.Start(1.0f, 0.1f);
    }

    private void Update()
    {
        UpdateLocation();
    }

    public void UpdateLocation()
    {
        if (Input.location.isEnabledByUser)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;

            latitudeText.text = "Latitude: " + latitude;
            longitudeText.text = "Longitude: " + longitude;
        }
        else
        {
            latitudeText.text = "User has not enabled the geolocation.";
        }
    }
}