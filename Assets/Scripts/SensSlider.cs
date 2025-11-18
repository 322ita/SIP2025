using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensSlider : MonoBehaviour
{
    Slider slider;
    public TMP_Text testo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("Sens", 10f);
        testo.SetText(slider.value.ToString());
        slider.onValueChanged.AddListener(changeSlider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeSlider(float input)
    {
        PlayerPrefs.SetFloat("Sens", input);
        testo.SetText(slider.value.ToString());
    }
}
