using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.text = PlayerPrefs.GetString("PlrName");
        inputField.onEndEdit.AddListener(onEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onEnd(string risposta)
    {
        inputField.text = risposta;
        PlayerPrefs.SetString("PlrName", risposta);
    }
}
