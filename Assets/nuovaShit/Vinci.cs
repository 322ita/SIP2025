using UnityEngine;

public class Vinci : MonoBehaviour
{
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
            GameObject.FindAnyObjectByType<GameManager>().StopTimer();
            UnityEngine.SceneManagement.SceneManager.LoadScene(8);
        }
        
    }
}
