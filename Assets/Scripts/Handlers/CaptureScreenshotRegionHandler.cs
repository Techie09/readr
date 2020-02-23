using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 
/// </summary>
/// <seealso cref="https://gamedev.stackexchange.com/questions/116258/unity3d-screen-capture-of-particular-ui-elements"/>
public class CaptureScreenshotRegionHandler : MonoBehaviour
{
    public RectTransform rectT; // Assign the UI element which you wanna capture
    int width; // width of the object to capture
    int height; // height of the object to capture

    // Use this for initialization
    void Start()
    {
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        //allow width and height to be dynamically different each frame
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(takeScreenShot()); // screenshot of a particular UI Element.
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ScreenCapture.CaptureScreenshot("FullPageScreenShot.png");
        }

    }
    public IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame(); // it must be a coroutine 

        Vector2 temp = rectT.transform.position;
        var startX = temp.x - width / 2;
        var startY = temp.y - height / 2;

        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(Application.dataPath + "ScreenShot.png", bytes);

    }
}
