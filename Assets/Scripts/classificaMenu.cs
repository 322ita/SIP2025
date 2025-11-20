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

    }

    // Update is called once per frame
    void Update()
    {
        timeLeaderBoard.LevelName = livelli[pos];
    }



}
