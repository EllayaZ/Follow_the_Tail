using UnityEngine;

public class PlacePrefab : MonoBehaviour
{
    public GameObject myPrefab;

    public void PlaceObject()
    {
        Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}