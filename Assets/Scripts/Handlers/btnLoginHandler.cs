using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        btn.onClick.AddListener(() => AttemptLogin());
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public async void AttemptLogin()
    {
        try {
            Debug.Log("btnLogin clicked");

            string username = txtUsername.text;
            if (!String.IsNullOrWhiteSpace(username))
            {
                try
                {
                    var appUserResult = await _appUserClient.LoginAppUser(username);
                    if (appUserResult != null)
                    {
                        //redirect to next scene => Session Setup
                    }
                    else
                    {
                        lblMessage.text = $"Error occured and {username} could not login";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.text = ex.Message;
                }

            }

        } catch(Exception ex) {
            //add logging
        }
    }
}
