﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnCreateAccountHandler : MonoBehaviour
{
    public Text lblMessage;
    public InputField txtUsername;
    public Button btnCreateAccount;

    private AppUserController _appUserClient;

    public btnCreateAccountHandler()
    {
        _appUserClient = new AppUserController();
    }


    // Start is called before the first frame update
    void Start()
    {
        Button btn = btnCreateAccount.GetComponent<Button>();
        btn.onClick.AddListener(() => AttemptCreateAccount());
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public async void AttemptCreateAccount()
    {
        try
        {
            Debug.Log("btnCreateAccount clicked");

            string username = txtUsername.text;
            if (!String.IsNullOrWhiteSpace(username))
            {
                try
                {
                    await _appUserClient.AddAppUserAsync(username);
                    lblMessage.text = $"{username} was added successfully";
                }
                catch (Exception ex)
                {
                    lblMessage.text = ex.Message;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
}
