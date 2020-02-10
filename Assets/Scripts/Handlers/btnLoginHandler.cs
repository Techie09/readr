using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        btn.onClick.AddListener(AttemptLogin);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void AttemptLogin()
    {
        Debug.Log("btnLogin clicked");

        string username = txtUsername.text;
        if(!String.IsNullOrWhiteSpace(username))
        {
            try
            {
                var appUserResult = _appUserClient.AddAppUser(username);
                if(appUserResult != null)
                {
                    lblMessage.text = $"{username} was added successfully";  
                }
                else
                {
                    lblMessage.text = $"Error occured and {username} could not be added";
                }
            }
            catch (Exception ex)
            {
                lblMessage.text = ex.Message;
            }
            
        }
    }
}
