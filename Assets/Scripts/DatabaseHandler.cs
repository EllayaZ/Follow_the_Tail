using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Proyecto26;
using ARLocation;

[RequireComponent(typeof(LocationService))]
public class DatabaseHandler : MonoBehaviour
{
    private EntriesContainer locations = new EntriesContainer();
    public LocationService locationService;
    private WebMapLoader webMapLoader;

    private void Awake()
    {
        StartCoroutine(GetLocations());
        locationService = gameObject.GetComponent<LocationService>();
        webMapLoader = gameObject.GetComponent<WebMapLoader>();
        //PostNewLocation(32, 49.279264, -123.110048, "Pizza");
    }

    IEnumerator GetLocations()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://follow-the-tail.firebaseio.com/Locations/.json");
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        while (!www.downloadHandler.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        if (www.isHttpError || www.isNetworkError)
        {
            Debug.LogError(www.error);
        }

        else
        {
            string locationsString = www.downloadHandler.text;
            SetLocations(locationsString);
        }

        yield break;
    }

    private void SetLocations(string json)
    {
        locations = JsonConvert.DeserializeObject<EntriesContainer>(json);
        XmlOperation.Serialize(locations, "Assets/Data/NewData.xml");
    }

    public void PostLocation(string meshID)
    {
        int newID = locations.entriesContainer[locations.entriesContainer.Count - 1].id + 1;

        Entry entry = new Entry()
        {
            id = newID,
            lat = locationService.latitude,
            lng = locationService.longitude,
            altitude = 0,
            altitudeMode = "GroundRelative",
            name = "entry",
            meshId = meshID,
            movementSmoothing = 0,
            maxNumberOfLocationUpdates = 1,
            useMovingAverage = false,
            hideObjectUtilItIsPlaced = false
        };

        locations.entriesContainer.Add(entry); 
        RestClient.Put("https://follow-the-tail.firebaseio.com/Locations/entriesContainer/" + newID + ".json", entry);
    }

    public void PostNewLocation(int id, double lat, double lng, string meshid)
    {
        Debug.Log("Posting");
        int newID = id;
        Entry entry = new Entry()
        {
            id = newID,
            lat = lat,
            lng = lng,
            altitude = 0,
            altitudeMode = "GroundRelative",
            name = "entry",
            meshId = meshid,
            movementSmoothing = 0,
            maxNumberOfLocationUpdates = 1,
            useMovingAverage = false,
            hideObjectUtilItIsPlaced = false
        };

        locations.entriesContainer.Add(entry);
        RestClient.Put("https://follow-the-tail.firebaseio.com/Locations/entriesContainer/" + newID + ".json", entry);
    }
}