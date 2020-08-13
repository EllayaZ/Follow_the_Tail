using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class LoginHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TMP_InputField userPassText;
    [SerializeField] private TextMeshProUGUI usernameWelcomeText;

    [SerializeField] private GameObject wrongPasswordPanel;
    [SerializeField] private GameObject LoginPanel;
    [SerializeField] private GameObject SelectedExperiencePanel;

    public void LoginUser()
    {
        StartCoroutine(GetUser());
    }

    IEnumerator GetUser()
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
                wrongPasswordPanel.SetActive(true);
            }

            else
            {
                // Getting the user:
                User thisUser = JsonConvert.DeserializeObject<User>(userString);

                // Comparing passwords:
                if (string.Equals(CleanInput(userPassText.text.ToString()), thisUser.password))
                {
                    usernameWelcomeText.text = "Hi " + CleanInput(usernameText.text.ToString());
                    SelectedExperiencePanel.SetActive(true);
                    LoginPanel.SetActive(false);
                }

                else
                {
                    wrongPasswordPanel.SetActive(true);
                }
            }
        }

        yield break;
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
