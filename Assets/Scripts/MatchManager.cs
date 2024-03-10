using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{

    private PlayerManager Pm;
    [SerializeField]private int MaxTimer;
    [SerializeField] public static float currentTimer;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        currentTimer = MaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
    }
}
