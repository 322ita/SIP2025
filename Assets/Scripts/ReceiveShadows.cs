using UnityEngine;

public class ReceiveShadows : MonoBehaviour
{
    MeshRenderer render;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        render.receiveShadows = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
