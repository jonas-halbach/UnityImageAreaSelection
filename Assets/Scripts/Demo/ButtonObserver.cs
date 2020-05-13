using UnityEngine;
public class ButtonObserver : MonoBehaviour
{
    [SerializeField]
    private Texture2D defaultTexture;

    MeshRenderer renderer;

    private WebCamTexture webCam;

    void Start() {
        renderer = GetComponent<MeshRenderer>();
        webCam = new WebCamTexture(WebCamTexture.devices[0].name);
    }

    public void OnActivateCameraButtonClick() {
        webCam.Play();
        renderer.material.mainTexture = webCam;
    }

    public void OnResetToDeaultButtonClick() {
        webCam.Stop();
        renderer.material.mainTexture = defaultTexture;

    }
}
