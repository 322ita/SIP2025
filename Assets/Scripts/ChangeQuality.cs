using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuality : MonoBehaviour
{
    TMP_Dropdown dropdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(changeSettings);
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void changeSettings(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }
}
