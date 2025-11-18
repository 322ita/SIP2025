using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerClickHandler : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    [SerializeField] float dist = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonUp(0)) {
            if (Physics.Raycast(ray, out hit, dist))
            {
                MouseHandler();
            }
        }
    }

    void MouseHandler()
    {
        if(hit.collider != null)
        {
            SceneLoader sceneLoader = hit.collider.GetComponent<SceneLoader>();
            if (sceneLoader != null)
                sceneLoader.LoadScene();
            if (hit.collider.CompareTag("circuito"))
            {
                GameObject.FindFirstObjectByType<GameManager>().StartTimer();
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.CompareTag("foddi"))
            {
                
                if (GameObject.FindFirstObjectByType<GameManager>().timerOn)
                {
                    GameObject.FindFirstObjectByType<GameManager>().mrf.SetActive(false);
                    GameObject.FindFirstObjectByType<GameManager>().mrfok.SetActive(true);
                }
                GameObject.FindFirstObjectByType<GameManager>().StopTimer();
            }
        }
    }
}
