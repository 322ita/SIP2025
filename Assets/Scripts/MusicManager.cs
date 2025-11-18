using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Volume volume;
    [SerializeField] PlayerMovement mPlayerMovement;
    [SerializeField] float dampedHz = 500, normalHz = 22000;
    [SerializeField] float lowVol = -20f, norVol = -5;
    [SerializeField] float lowCam = 42f, norCam = 0;
    [SerializeField] float smooth = 10, smooth2 = 5;
    // Start is called before the first frame update
    void Start()
    {
        volume = GameObject.FindFirstObjectByType<Volume>();
        musicSource = GetComponent<AudioSource>();
        mPlayerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHz;
        float lerpedHz;
        float currentVol;
        float currentLD;
        float currentCA;
        float lerpedVol;
        float lerpedLD;
        float lerpedCA;
        LensDistortion lensDistortion;
        ChromaticAberration chromaticAberration;

        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out chromaticAberration);

        audioMixer.GetFloat("lowpass", out currentHz);
        audioMixer.GetFloat("volume", out currentVol);
        currentLD = lensDistortion.intensity.value;
        currentCA = chromaticAberration.intensity.value;

        if (mPlayerMovement != null) {
            float magnitudo = mPlayerMovement.GetComponent<Rigidbody>().linearVelocity.magnitude;
            float targetHz = (magnitudo < 1) ? dampedHz : normalHz;
            float targetVol = (magnitudo < 1) ? lowVol : norVol;
            float targetLD = (magnitudo < 1) ? lowCam : norCam;
            float targetCA = (magnitudo < 1) ? lowCam : norCam;

            lerpedHz = Mathf.Lerp(currentHz, targetHz, smooth * Time.deltaTime);
            lerpedVol = Mathf.Lerp(currentVol, targetVol, smooth * Time.deltaTime);
            lerpedLD = Mathf.MoveTowards(currentLD, targetLD, smooth2 * Time.deltaTime);
            lerpedCA = Mathf.MoveTowards(currentCA, targetCA, smooth2 * Time.deltaTime);

            audioMixer.SetFloat("lowpass", lerpedHz);
            audioMixer.SetFloat("volume", lerpedVol);

            lensDistortion.intensity.Override(lerpedLD);
            chromaticAberration.intensity.Override(lerpedCA);

            
        }

    }
}
