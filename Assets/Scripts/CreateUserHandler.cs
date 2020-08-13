using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Proyecto26;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class CreateUserHandler : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI usernameText;
    [SerializeField] private TMP_InputField userPassText;
    [SerializeField] private TMP_InputField userPass2Text;
    [SerializeField] private TextMeshProUGUI usernameWelcomeText;

    [SerializeField] private GameObject nameTakenPopUp;
    [SerializeField] private GameObject CreateAccountPanel;
    [SerializeField] private GameObject SelectedExperiencePanel;

    public void PostUser()
    {
        StartCoroutine(CheckUser());
    }

    IEnumerator CheckUser()
    {
        string username = CleanInput(usernameText.text.ToString());
        username = (username.Length > 0) ? username : "-";
        string api = "https://follow-the-tail.firebaseio.com/Users/" + username + ".json";

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
            if (userString == "null")
            {
                CreateUser();
            }

            else
            {
                nameTakenPopUp.SetActive(true);
            }
        }

        yield break;
    }

    private void CreateUser()
    {
        Debug.Log("Password: " + CleanInput(userPassText.text.ToString()));
        Debug.Log("Password 2: " + CleanInput(userPass2Text.text.ToString()));
        if (string.Equals(CleanInput(userPassText.text.ToString()), CleanInput(userPass2Text.text.ToString())))
        {
            User newUser = new User()
            {
                password = CleanInput(userPassText.text.ToString())
            };

            // Posting the user:
            RestClient.Put("https://follow-the-tail.firebaseio.com/Users/" + CleanInput(usernameText.text.ToString()) + ".json", newUser);

            usernameWelcomeText.text = "Hi " + CleanInput(usernameText.text.ToString());
            SelectedExperiencePanel.SetActive(true);
            CreateAccountPanel.SetActive(false);
        }

        else
        {
            Debug.Log("Passwords doesn't match");
            // We need a new Pop Up 
        }
    }

    static string CleanInput(string strIn)
    {
        // Replace invalid characters with empty strings.
        try
        {
            return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                 RegexOptions.None, TimeSpan.FromSeconds(1.5));
        }
        // If we timeout when replacing invalid characters,
        // we should return Empty.
        catch (RegexMatchTimeoutException)
        {
            return String.Empty;
        }
    }
}
