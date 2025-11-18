using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    PlayerMovement playerMovement;
    CameraController cameraController;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
        cameraController = GameObject.FindFirstObjectByType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) )
        {
            if (!menu.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                menu.SetActive(true);
                Time.timeScale = 0f;
                playerMovement.enabled = false;
                cameraController.enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                menu.SetActive(false);
                Time.timeScale = 1f;
                playerMovement.enabled = true;
                cameraController.enabled = true;
            }
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuSelector()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
