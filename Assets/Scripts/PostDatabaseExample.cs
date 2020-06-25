using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Proyecto26;

public class PostDatabaseExample : MonoBehaviour
{
    void Start()
    {
        EntriesContainer entriesContainer = new EntriesContainer();
        Entry entry1 = new Entry()
        {
            id = 10001,
            lat = 49.287612915039063f,
            lng = -123.12663269042969f,
            altitude = 0f,
            altitudeMode = "GroundRelative",
            name = "entry",
            meshId = "Cube",
            movementSmoothing = 0,
            maxNumberOfLocationUpdates = 1,
            useMovingAverage = false,
            hideObjectUtilItIsPlaced = false
        };

        Entry entry2 = new Entry()
        {
            id = 10002,
            lat = 49.287612915039067f,
            lng = -123.12663269042970f,
            altitude = 0f,
            altitudeMode = "GroundRelative",
            name = "entry",
            meshId = "Sphere",
            movementSmoothing = 0,
            maxNumberOfLocationUpdates = 1,
            useMovingAverage = false,
            hideObjectUtilItIsPlaced = false
        };

        entriesContainer.entriesContainer.Add(entry1);
        entriesContainer.entriesContainer.Add(entry2);
        RestClient.Put("https://follow-the-tail.firebaseio.com/Locations/.json", entriesContainer);
    }
}
