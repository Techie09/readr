using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class txtDurationHandler : MonoBehaviour
{
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text obj = this.gameObject.GetComponent<Text>();
        if (obj.IsActive())
        {
            duration += Time.deltaTime;
        }
        
        var hours = ((int)duration / 3600).ToString();
        var minutes = ((int)duration / 60).ToString();
        var seconds = ((int)duration % 60).ToString();
        obj.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}
