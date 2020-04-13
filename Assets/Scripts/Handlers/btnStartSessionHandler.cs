using System;
using Readr.Assets.Scripts;
using Readr.Assets.Scripts.Controllers;
using Readr.Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

public class btnStartSessionHandler : MonoBehaviour
{
    //Panels do not have a unique class and are treated as just gameobjects in Unity
    public GameObject pnlSetupSession; 
    public Text lblMessage;
    public InputField txtIsbn;
    public InputField txtDescription;
    public Button btnStartSession;

    private UserSessionController _userSessionClient;

    public btnStartSessionHandler()
    {
        _userSessionClient = new UserSessionController();
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = btnStartSession.GetComponent<Button>();
        btn.onClick.AddListener(() => StartSessionAsync());
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public async void StartSessionAsync()
    {
        try
        {
            Debug.Log("btnStartSession clicked");
            
            string isbn = txtIsbn.text;
            string description = txtDescription.text;

            var newUserSession = new UserSession
            {
                Isbn = txtIsbn.text,
                Description = txtDescription.text,
                ModifiedById = AppSession.Current.AppUser.id
            };

            var userSessionResult = await _userSessionClient.CreateSessionAsync(newUserSession);
            if(userSessionResult != null)
            {
                Debug.Log("Session started successfully");
                var result = AppSession.Current.SetCurrentSession(userSessionResult);
                if(result != null)
                {
                    AppSession.Current.sessionState = SessionState.Running;
                    pnlSetupSession.SetActive(false);
                }
                else
                {
                    //What to do here?!?!?
                }
            }
            else
            {
                Debug.LogWarning("error from result when starting session");
                lblMessage.text = $"Error occured and session could not be started";
            }

        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            lblMessage.text = ex.Message;
        }
        

    }

    void OnDestroy()
    {
        lblMessage = null;
        txtIsbn = null;
        txtDescription = null;
        btnStartSession = null;
        _userSessionClient = null;
    }
}
