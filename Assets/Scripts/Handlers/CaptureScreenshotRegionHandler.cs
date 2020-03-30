using System.Collections;
using System.IO;
using System.Linq;
using Readr.Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.Windows.WebCam;

/// <summary>
/// 
/// </summary>
/// <seealso cref="https://gamedev.stackexchange.com/questions/116258/unity3d-screen-capture-of-particular-ui-elements"/>
public class CaptureScreenshotRegionHandler : MonoBehaviour
{
    public RectTransform rectT; // Assign the UI element which you wanna capture\
    public string screenshotFileName = "screenshot.png";

    private PhotoCapture photoCaptureObject;
    private AzureController _azureController;
    private int _width; // width of the object to capture
    private int _height; // height of the object to capture

    // Use this for initialization
    void Start()
    {
        _azureController = new AzureController();
        _width = System.Convert.ToInt32(rectT.rect.width);
        _height = System.Convert.ToInt32(rectT.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        //allow width and height to be dynamically different each frame
        _width = System.Convert.ToInt32(rectT.rect.width);
        _height = System.Convert.ToInt32(rectT.rect.height);

        ExecuteImageCaptureAndAnalysis();
    }
    public IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame(); // it must be a coroutine 

        Vector2 temp = rectT.transform.position;
        var startX = temp.x - _width / 2;
        var startY = temp.y - _height / 2;

        var tex = new Texture2D(_width, _height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(startX, startY, _width, _height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(GetScreenshotFilePath(), bytes);
    }

    private string GetScreenshotFilePath()
    {
        return Path.Combine(Application.dataPath + "/../", screenshotFileName);
    }

    /// <summary>    
    /// Begin process of Image Capturing and send To Azure     
    /// Computer Vision service.   
    /// </summary>    
    private void ExecuteImageCaptureAndAnalysis()
    {
        var isNewCapture = false;
        if (AppSession.Capabilities.isWindowsMR)
        {
            isNewCapture = TakeScreenshotMixedReality();
        }
        else if (AppSession.Capabilities.isHololens)
        {
            isNewCapture = TakeScreenshotHololens();
        }
        else //we assume PC
        {
            isNewCapture = TakeScreenshotPC();
        }

        if (isNewCapture)
        {
            AnalysedObject result = null;
            var taskResult = _azureController.PostVisionAnalysisAsync<AnalysedObject>(GetScreenshotFilePath());
            taskResult.ContinueWith(s => result = s.Result);
        }
    }

    private bool TakeScreenshotPC()
    {
        var isCtrlKeyDown = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        if (isCtrlKeyDown && Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(takeScreenShot()); // screenshot of a particular UI Element.
            return true;
        }
        if (isCtrlKeyDown && Input.GetKeyDown(KeyCode.F3))
        {
            ScreenCapture.CaptureScreenshot(GetScreenshotFilePath());
            return true;
        }
        return false;
    }

    private bool TakeScreenshotMixedReality()
    {
        var filePath = GetScreenshotFilePath();
        if (File.Exists(filePath) && !IsFileLocked(filePath))
        {
            File.Delete(filePath);
        }

        ScreenCapture.CaptureScreenshot(screenshotFileName, ScreenCapture.StereoScreenCaptureMode.LeftEye);
        return true;

        ////send image to API
        //_ = _azureController.PostVisionAnalysisAsync<AnalysedObject>(filePath);
    }

    private bool TakeScreenshotHololens()
    {
        // Set the camera resolution to be the highest possible    
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

        Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Begin capture process, set the image format    
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject)
        {
            photoCaptureObject = captureObject;
            CameraParameters camParameters = new CameraParameters();
            camParameters.hologramOpacity = 0.0f;
            camParameters.cameraResolutionWidth = targetTexture.width;
            camParameters.cameraResolutionHeight = targetTexture.height;
            camParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Capture the image from the camera and save it in the App internal folder    
            captureObject.StartPhotoModeAsync(camParameters, delegate (PhotoCapture.PhotoCaptureResult result)
            {
                string filename = string.Format(@"CapturedImage.jpg");

                string filePath = Path.Combine(Application.persistentDataPath, filename);

                //SEt the filePath for the Api call to find
                //VisionManager.instance.imagePath = filePath;

                //take picture and save to file.
                //photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);

                //stop capturing
                //currentlyCapturing = false;                
            });
        });
        return true;
    }

    /// <summary>
    /// Register the full execution of the Photo Capture. If successful, it will begin 
    /// the Image Analysis process.
    /// </summary>
    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        // Call StopPhotoMode once the image has successfully captured
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Dispose from the object in memory and request the image analysis 
        // to the VisionManager class
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }


    protected virtual bool IsFileLocked(string filePath)
    {
        FileStream stream = null;

        try
        {
            stream = new FileInfo(filePath).Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }
}
