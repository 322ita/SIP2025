using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum opzioni
{
    inizio,
    fine
}

public class TimeScript : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] opzioni opzioni;
    private void Awake()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (opzioni == opzioni.inizio)
            gameManager.StartTimer();
        else if (opzioni == opzioni.fine) gameManager.StopTimer();
    }

}
