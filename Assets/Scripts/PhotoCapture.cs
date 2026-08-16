using UnityEngine;

public class PhotoCapture : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //InvokeRepeating("PhotoCapturing", 2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PhotoCapturing();
            print("Photo Captured!");
        }
    }

    public void PhotoCapturing()
    {
        string folderPath = "Assets/Photos/";
        if(!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
        var screenshotName = "Photo_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName), 2);
        Debug.Log(folderPath + screenshotName);
    }
}