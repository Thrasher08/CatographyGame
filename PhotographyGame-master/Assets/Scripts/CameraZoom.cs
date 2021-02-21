using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{

    public CinemachineVirtualCamera cam;
    public GameObject zoomIndicator;
    public GameObject focus;
    public Volume ppv;
    //public PostProcess

    Vignette vignette;
    float noVignette = 0;
    public float zoomedVignette;
    public float initialVignette;

    FilmGrain filmGrain;
    float noFilmGrain = 0;
    public float zoomedGrain;
    public float initialGrain;

    float initialFOV;
    public float zoomedFOV;
    public float zoomSpeed;

    float startInputTime = 0;
    public float holdTime;

    bool zooming = false;
    bool unZooming = false;
    bool takingPhoto = false;


    // Start is called before the first frame update
    void Start()
    {
        initialFOV = cam.m_Lens.FieldOfView;

        ppv.profile.TryGet<Vignette>(out vignette);
        vignette.intensity.value = initialVignette;

        ppv.profile.TryGet<FilmGrain>(out filmGrain);
        filmGrain.intensity.value = noFilmGrain;
    }

    private void OnDisable()
    {
        vignette.intensity.value = noVignette;
        filmGrain.intensity.value = noFilmGrain;
    }
    private void OnEnable()
    {
        if (vignette == null || filmGrain == null) return;
        vignette.intensity.value = initialVignette;
        filmGrain.intensity.value = initialGrain;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("r"))
        {
            zooming = true;
        }
        else
        {
            zooming = false;
        }

        if (Input.GetKey("t"))
        {
            unZooming = true;
        }
        else
        {
            unZooming = false;
        }

        if (zooming)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, zoomedFOV, Time.deltaTime * zoomSpeed);
            zoomIndicator.transform.localPosition = new Vector3(zoomIndicator.transform.localPosition.x, Mathf.Lerp(zoomIndicator.transform.localPosition.y, 160f, Time.deltaTime * zoomSpeed), zoomIndicator.transform.localPosition.z);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, zoomedVignette, Time.deltaTime * zoomSpeed);
            filmGrain.intensity.value = Mathf.Lerp(filmGrain.intensity.value, zoomedGrain, Time.deltaTime * zoomSpeed);
        }

        if (unZooming)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, initialFOV, Time.deltaTime * zoomSpeed);
            zoomIndicator.transform.localPosition = new Vector3(zoomIndicator.transform.localPosition.x, Mathf.Lerp(zoomIndicator.transform.localPosition.y, 0f, Time.deltaTime * zoomSpeed), zoomIndicator.transform.localPosition.z);
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, initialVignette, Time.deltaTime * zoomSpeed);
            filmGrain.intensity.value = Mathf.Lerp(filmGrain.intensity.value, initialGrain, Time.deltaTime * zoomSpeed);
        }

        if (Input.GetKeyDown("y"))
        {
            focus.GetComponent<Image>().color = new Color32(0, 255, 0, 200);
            startInputTime = Time.time;
            takingPhoto = true;
        }

        if (Input.GetKey("y"))
        {
            if ((startInputTime + holdTime <= Time.time) && takingPhoto)
            {
                focus.GetComponent<Image>().color = new Color32(255, 0, 0, 200);
                // save photo

            }
        }
        else
        {
            focus.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            takingPhoto = false;
        }

    }

    IEnumerator waitForPhoto(float inputTime)
    {
        yield return new WaitForSeconds(5);
    }
}