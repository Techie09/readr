using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnLoginHandler : MonoBehaviour
{
    public Text lblMessage { get; set; }
    public InputField txtUsername { get; set; }

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void AttemptLogin()
    {
        string username = txtUsername.text;

    }
}
