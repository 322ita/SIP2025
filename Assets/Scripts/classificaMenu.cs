using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class classificaMenu : MonoBehaviour
{
    [SerializeField] Button buttonMinore;
    [SerializeField] Button buttonMaggiore;
    [SerializeField] TMP_Text text;

    [SerializeField] TimeLeaderBoard timeLeaderBoard;

    int pos = 0;
    [SerializeField]List<string> livelli = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonMinore.onClick.AddListener(minore);
        buttonMaggiore.onClick.AddListener(maggiore);
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(string.Format("Classifica\n{0}", livelli[pos]));
        timeLeaderBoard.LevelName = livelli[pos];
    }

    void maggiore()
    {
        if (pos == livelli.Count-1)
            pos = 0;
        else
            pos++;
    }
    void minore()
    {
        if (pos == 0)
            pos = livelli.Count - 1;
        else
            pos--;
    }

}
