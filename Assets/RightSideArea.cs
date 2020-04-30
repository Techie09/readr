using System.Collections;
using System.Collections.Generic;
using Readr.Assets.Scripts;
using UnityEngine;

public class RightSideArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AppSession.Current.sessionState != SessionState.Running)
        {
            this.enabled = false;
            return;
        }

        this.enabled = true;
    }
}
