using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTestPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = this.gameObject;
        obj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = this.gameObject;
        //Check if the Session has been initialized. TODO: later make a paused state?
        if (!string.IsNullOrWhiteSpace(AppSession.Current.UserSession?.Id))
        {
            obj.SetActive(true);
        }

        if (!obj.activeInHierarchy)
        {
            return;
        }
    }

    private void OnGUI()
    {
        GameObject obj = this.gameObject;


        if (!obj.activeInHierarchy)
        {
            return;
        }

        //var confidenceLevel = 0.98f;
        //var rect = new Rect(obj.gameObject.transform.position.x - 25, obj.gameObject.transform.position.y, 20, 20);
        //if (confidenceLevel >= 0.80)
        //{
        //    DrawQuad(rect, Color.green);
        //}
        //else if (confidenceLevel >= 0.60)
        //{
        //    DrawQuad(rect, Color.yellow);
        //}
        //else
        //{
        //    DrawQuad(rect, Color.red);
        //}
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
