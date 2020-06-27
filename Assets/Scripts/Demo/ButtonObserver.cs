using UnityEngine;
public class ButtonObserver : MonoBehaviour
{
    [SerializeField]
    private Texture2D defaultTexture;

    MeshRenderer backgroundRenderer;

    private WebCamTexture webCam;

    void Start() {
        backgroundRenderer = GetComponent<MeshRenderer>();
        webCam = new WebCamTexture(WebCamTexture.devices[0].name);
    }

    public void OnActivateCameraButtonClick() {
        webCam.Play();
        backgroundRenderer.material.mainTexture = webCam;
    }

    public void OnResetToDeaultButtonClick() {
        webCam.Stop();
        backgroundRenderer.material.mainTexture = defaultTexture;

    }
}
