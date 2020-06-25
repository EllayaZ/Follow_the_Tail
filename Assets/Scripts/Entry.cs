using System;

[Serializable]
public class Entry
{
    public int id;
    public double lat;
    public double lng;
    public double altitude;
    public string altitudeMode;
    public string name;
    public string meshId;
    public float movementSmoothing;
    public int maxNumberOfLocationUpdates;
    public bool useMovingAverage;
    public bool hideObjectUtilItIsPlaced;
}