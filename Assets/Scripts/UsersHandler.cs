using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Proyecto26;

public class UsersHandler : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI userPassText;
    [SerializeField] private TextMeshProUGUI userPass2Text;

    public void PostUser()
    {
        StartCoroutine(CheckUser());
        /*if (userPassText.text != userPass2Text.text)
        {

        }
        else
        {

        }*/
    }

    IEnumerator CheckUser()
    {
        string username = usernameText.text.ToString();
        username = "kal3b";
        string api = "https://follow-the-tail.firebaseio.com/Users/" + username + ".json";
        Debug.Log(api);
        UnityWebRequest www = UnityWebRequest.Get(api);
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
            string userString = www.downloadHandler.text;
            Debug.Log(userString);
        }

        yield break;
    }
}
