using UnityEngine;
using UnityEngine.UI;

public class CameraAccess : MonoBehaviour {
    private WebCamTexture cameraTexture;
    private RawImage textureBackground;
    private AspectRatioFitter fit;

    private bool camAvailable;

    private float lastMirror;
    private int lastRotation;

    public void InitializeCamera(RawImage background, bool frontFacing = false)
    {

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
            return;
        for (int i = 0; i < devices.Length; i++)
        {
            var curr = devices[i];

            if (curr.isFrontFacing == frontFacing)
            {
                this.cameraTexture = new WebCamTexture(curr.name, Screen.width, Screen.height);
                break;
            }
        }
        if (cameraTexture == null)
            return;
        cameraTexture.Play();
        background.texture = cameraTexture;
        this.textureBackground = background;
        this.fit = background.GetComponent<AspectRatioFitter>();
        camAvailable = true;
        
        if(Application.isMobilePlatform)
            textureBackground.uvRect = new Rect(-0.3333f, -0.3333f, 1.6666f, 1.6666f);
        else
            textureBackground.uvRect = new Rect(0, 0, 1, 1);
    }
    public void StopCamera()
    {
        cameraTexture.Stop();
        camAvailable = false;
    }
    public void AdjustCamera()
    {
        if (!camAvailable)
            return;
        FitAspectRatio();
        Mirror();
        Rotate();
    }
    private void Rotate()
    {
        int orient = -cameraTexture.videoRotationAngle;
        lastRotation = orient;
        textureBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
    private void Mirror()
    {
        float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f;
        lastMirror = scaleY;
        textureBackground.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
    }
    private void FitAspectRatio()
    {
        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        fit.aspectRatio = ratio;
    }
    public WebCamTexture GetWebCamTexture()
    {
        return cameraTexture;
    }
    public bool IsAvailable()
    {
        return camAvailable;
    }
    public Photo GetPicture()
    {
        Texture2D t2d = new Texture2D(cameraTexture.width, cameraTexture.height);
        t2d.SetPixels(cameraTexture.GetPixels());
        t2d.Apply();

        return new Photo(t2d);
    }
}