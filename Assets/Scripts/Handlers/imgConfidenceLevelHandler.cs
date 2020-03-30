using UnityEngine;
using UnityEngine.UI;

public class imgConfidenceLevelHandler : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AppSession.Current.sessionState != SessionState.Running)
        {
            image.enabled = false;
            return;
        }

        image.enabled = true;
        var confidenceLevel = 0.98f;
        if (confidenceLevel >= 0.80)
        {
            image.color = Color.green;
        }
        else if (confidenceLevel >= 0.60)
        {
            image.color = Color.yellow;
        }
        else
        {
            image.color = Color.red;
        }
    }
}
