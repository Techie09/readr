using Readr.Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class imgConfidenceLevelHandler : MonoBehaviour
{
    private Image image;
    private float duration;
    private float _currectConfidenceLevel;
    private float _newConfidenceLevel;

    public void SetConfidenceLevel(float confidenceLevel)
    {
        _newConfidenceLevel = confidenceLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = Color.gray;
        duration = 5;
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;

        if(_newConfidenceLevel > 0 && _currectConfidenceLevel != _newConfidenceLevel)
        {
            if (_newConfidenceLevel >= 0.80)
            {
                image.color = Color.green;
            }
            else if (_newConfidenceLevel >= 0.60)
            {
                image.color = Color.yellow;
            }
            else
            {
                image.color = Color.red;
            }
            _currectConfidenceLevel = _newConfidenceLevel;
            duration = 0;
            return;
        }

        if (duration > 5)
        {
            image.color = Color.gray;
            _newConfidenceLevel = -1;
        }
    }
}
