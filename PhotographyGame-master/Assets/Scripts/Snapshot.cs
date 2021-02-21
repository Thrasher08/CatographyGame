using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Snapshot : MonoBehaviour
{
    private static Snapshot instance;
    public static Snapshot Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Snapshot>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Snapshot";
                    instance = go.AddComponent<Snapshot>();
                }
            }
            return instance;
        }
    }

    public RenderTexture rt;

    Camera cam;

    bool fpsCam = false;

    public CinemachineFreeLook thirdPerson;
    public CinemachineVirtualCamera firstPerson;
    CameraZoom cameraZoom;
    public GameObject zoomUI;
    //public Volume postProcessVolume;

    public GameObject fpsController;
    public GameObject thirdPersonController;

    List<Snappable> inView;

    public void AddToInView(Snappable snappable)
    {
        if (!inView.Contains(snappable)) inView.Add(snappable);
    }
    public void RemoveFromInView(Snappable snappable)
    {
       if (inView.Contains(snappable)) inView.Remove(snappable);
    }
    private void Start()
    {
        cam = GetComponent<Camera>();
        inView = new List<Snappable>();
        cameraZoom = GetComponent<CameraZoom>();
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchCam();
        }
        if (Input.GetKeyDown(KeyCode.F9) && fpsCam)
        {
            StartCoroutine(CamCapture());
        }
    }

    void SwitchCam() {
        if (fpsCam)
        {
            thirdPerson.Priority = 15;
            firstPerson.Priority = 10;
            fpsCam = false;
            cameraZoom.enabled = false;
            zoomUI.SetActive(false);
            fpsController.GetComponent<FirstPersonController>().enabled = false;
            thirdPersonController.GetComponent<FreeLookUserInput>().enabled = true;
        }
        else {
            thirdPerson.Priority = 10;
            firstPerson.Priority = 15;
            fpsCam = true;
            cameraZoom.enabled = true;
            zoomUI.SetActive(true);
            fpsController.GetComponent<FirstPersonController>().enabled = true;
            thirdPersonController.GetComponent<FreeLookUserInput>().enabled = false;
        }
    }

    IEnumerator CamCapture()
    {
        //Vignette vignette;
        //postProcessVolume.profile.TryGet(out vignette);
        //vignette.active = false;

        yield return new WaitForEndOfFrame();
        RenderTexture tempRT = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 24);
            
        cam.targetTexture = tempRT;

        cam.Render();

        //vignette.active = true;

        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();

        List<Snappable> snappables = new List<Snappable>();
        foreach (Snappable snap in inView)
        {
            if (snap.GetComponent<Renderer>().IsVisibleFrom(cam))
                snappables.Add(snap);
        }
        if (snappables.Count < 1) SnapshotTracker.Instance.AddTextureOnly(image);
        else SnapshotTracker.Instance.AddSnappableWithTexture(snappables, image);


        cam.targetTexture = null;

        //rawImage.texture = image;
        Destroy(image);
    }
}