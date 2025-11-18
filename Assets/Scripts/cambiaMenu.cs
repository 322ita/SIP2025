using UnityEngine;
using UnityEngine.UI;

public class cambiaMenu : MonoBehaviour
{
    Button button;
    [SerializeField]int posizione;
    MainMenuCameraController mainCameraController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
        mainCameraController = GameObject.Find("Main Camera").GetComponent<MainMenuCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void onClick()
    {
        mainCameraController.posizione = posizione;
    }
}
