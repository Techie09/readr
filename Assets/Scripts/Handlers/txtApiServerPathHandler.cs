using UnityEngine;
using UnityEngine.UI;

public class txtApiServerPathHandler : MonoBehaviour
{
    public InputField txtApiServerPath;

    // Start is called before the first frame update
    void Start()
    {
        txtApiServerPath.onValueChanged.AddListener((s) => OnValueChanged(s));
        AppSession.Current.SetCurrentApiServerPath("http://localhost:5000");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void OnValueChanged(string value)
    {

        AppSession.Current.SetCurrentApiServerPath(value);
    }
}
