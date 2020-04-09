using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraMenu : MonoBehaviour {
    [SerializeField] private RawImage cameraFrame;
    [SerializeField] private CameraAccess cameraAccess;

    private WebCamTexture camTex;

    private bool frontFacingCam;

    public void Start()
    {
        frontFacingCam = Application.platform == RuntimePlatform.WindowsEditor;
        cameraAccess.InitializeCamera(cameraFrame, frontFacingCam);
        if (cameraAccess.IsAvailable())
            camTex = cameraAccess.GetWebCamTexture();
    }
    private void Update()
    {
        cameraAccess.AdjustCamera();
    }
    public void TakePhoto(){
        Photo photo = cameraAccess.GetPicture();
        if(Application.isMobilePlatform)
        {
            var t = TextureEdit.RotateClockwise(photo.GetAsTexture2D());
            photo.SetTexture(t);
        }
        FindObjectOfType<Gallery>().TakePhoto(photo);
    }
}