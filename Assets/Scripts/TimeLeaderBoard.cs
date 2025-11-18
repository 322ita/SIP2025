using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TypeShow
{
    local,
    global
}

public class TimeLeaderBoard : MonoBehaviour
{
    public TypeShow typeShow;
    public string LevelName;
    List<string> names = new List<string>();
    List<float> times = new List<float>();
    GameManager gameManager;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (typeShow == TypeShow.local)
            showLocal();
        else if (typeShow == TypeShow.global)
        {
            names = PlayerPrefsExtra.GetList<string>(LevelName + "Name", new List<string>());
            times = PlayerPrefsExtra.GetList<float>(LevelName + "Time", new List<float>());
            showGlobal();
        }
            
    }
    void showLocal()
    {
        text.SetText(string.Empty);
        for (int i = 0; i < gameManager.times.Count; i++) 
        {
            float minutes = Mathf.FloorToInt(gameManager.times[i] / 60);
            float seconds = Mathf.FloorToInt(gameManager.times[i] % 60);
            float ms = Mathf.FloorToInt((gameManager.times[i] % 1) * 1000);
            text.SetText(string.Format("{0}\n{1}: {2:00}:{3:00}:{4:000}", text.text, gameManager.names[i], minutes, seconds, ms));
        }
    }
    void showGlobal()
    {
        text.SetText(string.Empty);
        for (int i = 0; i < times.Count; i++)
        {
            float minutes = Mathf.FloorToInt(times[i] / 60);
            float seconds = Mathf.FloorToInt(times[i] % 60);
            float ms = Mathf.FloorToInt((times[i] % 1) * 1000);
            text.SetText(string.Format("{0}\n{1}: {2:00}:{3:00}:{4:000}", text.text, names[i], minutes, seconds, ms));
        }
    }
}
