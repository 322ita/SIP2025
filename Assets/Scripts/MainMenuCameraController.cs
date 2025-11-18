using System.Collections.Generic;
using UnityEngine;



public class MainMenuCameraController : MonoBehaviour
{
    public int posizione=0;
    public List<Transform> PosizioniCamere;
    [SerializeField] float smooth = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, PosizioniCamere[posizione].position, smooth*Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, PosizioniCamere[posizione].rotation, smooth * Time.deltaTime);
    }
}
