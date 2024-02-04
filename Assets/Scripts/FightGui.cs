using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGui : MonoBehaviour
{

    private int returnedTimeValue;
    [SerializeField] private float GUIHightPos;
    [SerializeField] private int FontSize;
    [SerializeField] private float timerH;
    [SerializeField] private float timerW;
    [SerializeField] private float GUIOffset;
    private Vector2 TimerSize;
    private GUIStyle skin;


    void Start()
    {
        GUIOffset = Screen.width / GUIOffset;
        TimerSize = new Vector2(Screen.width / timerW, Screen.width / timerH);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        returnedTimeValue = MatchManager.currentTimer;
    }

    private void OnGUI()
    {
        skin = new GUIStyle(GUI.skin.GetStyle("label"));

        skin.fontSize = Screen.width / FontSize;
        skin.alignment = TextAnchor.MiddleCenter;
        GUIHightPos = GUIOffset / 50;

        if (returnedTimeValue >= 10)

        {
            GUI.Label(new Rect(Screen.width / 2
           - (TimerSize.x / 2), GUIHightPos, TimerSize.x, TimerSize.y), returnedTimeValue.ToString(), skin);

        }
        if (returnedTimeValue < 10)

        {
            GUI.Label(new Rect(Screen.width / 2
           - (TimerSize.x / 2), GUIHightPos, TimerSize.x, TimerSize.y), "0" + returnedTimeValue.ToString(), skin);

        }

    }


}
