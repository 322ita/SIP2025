using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Atouas;
using UnityEngine.Animations;

public class Braccia : MonoBehaviour
{
    Andrew andrew;
    rics rics_;
    sam sam_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        andrew = GetComponent<Andrew>();
        rics_ = GetComponent<rics>();
        sam_ = GetComponent<sam>();

    }

    void Start()
    {
        switch (PlayerPrefs.GetString("characterSelected"))
        {
            case "Andrew":
                andrew.enabled = true;
                rics_.enabled = false;
                sam_.enabled = false;
                break;
            case "Rics":
                andrew.enabled = false;
                rics_.enabled = true;
                sam_.enabled = false;
                break;
            case "Sam":
                andrew.enabled = false;
                rics_.enabled = false;
                sam_.enabled = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
