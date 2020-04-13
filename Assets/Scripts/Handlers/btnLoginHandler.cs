using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Readr.Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnLoginHandler : MonoBehaviour
{
    public Text lblMessage;
    public InputField txtUsername;
    public Button btnLogin;

    private AppUserController _appUserClient;

    public btnLoginHandler()
    {
        _appUserClient = new AppUserController();
    }

    //// Start is called before the first frame update
    void Start()
    {
        Button btn = btnLogin.GetComponent<Button>();
        btn.onClick.AddListener(() => AttemptLoginAsync());
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    /// <summary>
    /// Handles on click event of button referenced to this script
    /// AtetmptLogin will check to see if the username has an existing account
    /// If the username exists as an AppUser then we have success
    /// If the username is not found we have failed
    /// </summary>
    public async void AttemptLoginAsync()
    {
        try 
        {
            Debug.Log("btnLogin clicked");

            string username = txtUsername.text;
            if (!String.IsNullOrWhiteSpace(username))
            {
                try
                {
                    var appUserResult = await _appUserClient.LoginAppUserAsync(username);
                    if (appUserResult != null)
                    {
                        Debug.Log($"Login Result: {appUserResult.id}");
                        Debug.Log("Login success! Redirecting to Session Setup Scene");
                        SceneManager.LoadSceneAsync("Session", LoadSceneMode.Single);
                    }
                    else
                    {
                        Debug.LogWarning("missing additional information from result");
                        lblMessage.text = $"Error occured and {username} could not login";
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                    lblMessage.text = ex.Message;
                }
            }
        } 
        catch(Exception ex) 
        {
            Debug.LogError(ex);
            throw;
        }
    }
}
