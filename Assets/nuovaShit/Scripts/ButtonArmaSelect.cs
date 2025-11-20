using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class ButtonArmaSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    [SerializeField] Texture statsImage;
    [SerializeField] RawImage defaultSprite;
    public string nome;

    void Start()
    {
        button = GetComponent<Button>();
        if (button == null) Debug.LogWarning("Button component not found on " + gameObject.name);
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SceneLoader sceneLoader = new SceneLoader();
        PlayerPrefs.SetString("characterSelected", nome);
        sceneLoader.LoadScene(1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered button: " + gameObject.name);
        defaultSprite.texture = statsImage;
        defaultSprite.color = new Color32(0,0,0,255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited button: " + gameObject.name);
        defaultSprite.texture = null;
        defaultSprite.color = new Color32(0,0,0,0);

    }
}
