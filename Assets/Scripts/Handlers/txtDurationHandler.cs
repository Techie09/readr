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
    }

    // Update is called once per frame
    void Update()
    {
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
