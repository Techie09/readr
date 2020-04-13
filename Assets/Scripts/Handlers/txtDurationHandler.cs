using Readr.Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class txtDurationHandler : MonoBehaviour
{
    private float duration;
    private Text obj;
    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
        obj = GetComponent<Text>();
        obj.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the Session has been initialized. TODO: later make a paused state?
        if (AppSession.Current.sessionState != SessionState.Running)
        {
            obj.enabled = false;
            return;
        }

        obj.enabled = true;
        UpdateSessionDuration();

        obj.text = GetTimerDisplay();
    }

    public void UpdateSessionDuration()
    {
        duration += Time.deltaTime;
    }

    public string GetTimerDisplay()
    {
        int hours = (int)duration / 3600;
        int minutes = (int)duration / 60;
        int seconds = (int)duration % 60;
        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}
