using UnityEngine;

public class Chiave : MonoBehaviour
{
    [SerializeField] string nome;
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
            collision.gameObject.GetComponent<ChiaviDisponibili>().chiavi.Add(nome);
            Destroy(gameObject);
        }
    }
}
