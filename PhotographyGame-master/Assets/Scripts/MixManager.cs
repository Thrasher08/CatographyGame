using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixManager : MonoBehaviour
{

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("p"))
        {
            float initialAmbVol;
            mixer.GetFloat("AmbientVol", out initialAmbVol);
            mixer.SetFloat("AmbientVol", Mathf.Lerp(initialAmbVol, Mathf.Log10(1f) * 20, Time.deltaTime * 1));
        }

        if (Input.GetKey("o"))
        {
            float initialVol;
            mixer.GetFloat("DrumsVol", out initialVol);
            mixer.SetFloat("DrumsVol", Mathf.Lerp(initialVol, Mathf.Log10(1f) * 20, Time.deltaTime * 1));
        }
    }
}