using System;
using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] String nome;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<ChiaviDisponibili>().chiavi.Contains(nome))
            {
                Destroy(gameObject);
            }
        }
    }
}
