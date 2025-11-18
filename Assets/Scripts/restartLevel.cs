using UnityEngine;


public class restartLevel : MonoBehaviour
{
    GameManager manager;
    [SerializeField] AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = manager.checkpoint.position;
            source.Play();
        }
    }
}
