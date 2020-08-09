using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Proyecto26;

[RequireComponent(typeof(LocationService))]
public class DatabaseHandler : MonoBehaviour
{
    private EntriesContainer locations = new EntriesContainer();
    public LocationService locationService;

    private void Awake()
    {
        StartCoroutine(GetLocations());
        locationService = gameObject.GetComponent<LocationService>();
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
        Debug.Log(json);
        locations = JsonConvert.DeserializeObject<EntriesContainer>(json);
        XmlOperation.Serialize(locations, "Assets/Data/NewData.xml");
        Debug.Log("Serializing");
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
}