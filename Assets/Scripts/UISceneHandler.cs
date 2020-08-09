using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject followOrWander;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashScreen());
    }

    IEnumerator SplashScreen()
    {
        splashScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        splashScreen.SetActive(false);
        followOrWander.SetActive(true);
    }
}
