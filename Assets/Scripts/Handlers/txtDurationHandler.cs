using UnityEngine;
using UnityEngine.UI;

public class txtDurationHandler : MonoBehaviour
{
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
        Text obj = this.gameObject.GetComponent<Text>();
        obj.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Text obj = this.gameObject.GetComponent<Text>();
        //Check if the Session has been initialized. TODO: later make a paused state?
        if (!string.IsNullOrWhiteSpace(AppSession.Current.UserSession?.Id))
        {
            obj.enabled = true;
        }

        if (!obj.isActiveAndEnabled)
        {
            return;
        }

        UpdateSessionDuration();

        obj.text = GetTimerDisplay();
    }

    public void UpdateSessionDuration()
    {
        duration += Time.deltaTime;
    }

    public string GetTimerDisplay()
    {
        var hours = ((int)duration / 3600).ToString();
        var minutes = ((int)duration / 60).ToString();
        var seconds = ((int)duration % 60).ToString();
        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    private void OnGUI()
    {
        Text obj = this.gameObject.GetComponent<Text>();


        if (!obj.isActiveAndEnabled)
        {
            return;
        }

        var confidenceLevel = 0.98f;
        var rect = new Rect(obj.gameObject.transform.position.x - 25, obj.gameObject.transform.position.y, 20, 20);
        if (confidenceLevel >= 0.80)
        {
            DrawQuad(rect, Color.green);
        }
        else if (confidenceLevel >= 0.60)
        {
            DrawQuad(rect, Color.yellow);
        }
        else
        {
            DrawQuad(rect, Color.red);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="https://answers.unity.com/questions/37752/how-to-render-a-colored-2d-rectangle.html"/>
    /// <param name="position"></param>
    /// <param name="color"></param>
    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}
