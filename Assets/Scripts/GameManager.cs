using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    PlayerMovement plr;
    Rigidbody rb;
    public bool timerOn=false;
    public float timer=0f;
    public TMP_Text TimeText;
    public TMP_Text velocityText;
    public TMP_Text statoText;
    public string Plrname = string.Empty;
    public string[] levels;
    [SerializeField]string LevelString;
    public List<string> names = new List<string>();
    public List<float> times = new List<float>();
    public Transform checkpoint;
    public GameObject mrf;
    public GameObject mrfok;
    // Use this for initialization
    void Start()
    {
        
        Time.timeScale = 1f;
        if (LevelString != "menu")
        {
            plr = GameObject.FindFirstObjectByType<PlayerMovement>();
            rb = plr.GetComponent<Rigidbody>();
            TimeText = GameObject.Find("PlrUI").transform.Find("Time").GetComponent<TMP_Text>();
            velocityText = GameObject.Find("PlrUI").transform.Find("Velocity").GetComponent<TMP_Text>();
            statoText = GameObject.Find("PlrUI").transform.Find("Stato").GetComponent<TMP_Text>();
            Plrname = PlayerPrefs.GetString("PlrName", "Atos");
        }
        
        
        names = PlayerPrefsExtra.GetList<string>(LevelString + "Name", new List<string>());
        times = PlayerPrefsExtra.GetList<float>(LevelString + "Time", new List<float>());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
        }
        DisplayTime();
        DisplayVelocity();
        DisplayStato();
    }
    public void StartTimer()
    {
        if (timerOn == false)
        {
            Debug.LogWarning("TIMESTARTED");
            timer = 0;
            timerOn = true;
        }
        
    }
    public void StopTimer()
    {
        if(timerOn == true)
        {
            Debug.LogWarning("TIMESTOPPED");
            timerOn = false;
            CercaGiocatore();
            sort();


            PlayerPrefsExtra.SetList(LevelString + "Name", names);
            PlayerPrefsExtra.SetList(LevelString + "Time", times);
        }
        
    }
    void DisplayTime()
    {
        if (TimeText != null)
        {
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            float ms = Mathf.FloorToInt((timer % 1) * 1000);
            TimeText.SetText(string.Format("Timer: {0:00}:{1:00}:{2:000}", minutes, seconds, ms));
        }
        
    }
    void DisplayVelocity()
    {
        if(velocityText != null)
        {
            float velocity = rb.linearVelocity.magnitude;
            velocityText.SetText(string.Format("Velocità: {0:00.00}", velocity));
        }
    }
    void DisplayStato()
    {
        if (statoText != null)
        {
            float velocity = rb.linearVelocity.magnitude;
            statoText.SetText(string.Format("Stato: {0}", plr.stateM.ToString()));
        }
    }
    void CercaGiocatore()
    {
        bool trovato=false;
        int i =0;
        for(i=0; i<times.Count; i++)
        {
            if (Plrname == names[i]) { 
                trovato = true; break;
            }
        }
        if (trovato)
        {
            if (timer < times[i])
                times[i] = timer;
        }
            
        else
        {
            names.Add(Plrname);
            times.Add(timer);
        }
    }
    void sort()
    {
        List<(string name2, float times2)> players = new List<(string, float)>();
        for (int i = 0; i < names.Count; i++)
            players.Add((names[i], times[i]));
        players.Sort((player1, player2) => player1.times2.CompareTo(player2.times2));
        int maxCount = Mathf.Min(players.Count, 10);
        names.Clear();
        times.Clear();
        for(int i = 0;i < maxCount; i++)
        {
            names.Add(players[i].name2);
            times.Add(players[i].times2);
        }
    }

    public void Esci()
    {
        Application.Quit();
    }
}