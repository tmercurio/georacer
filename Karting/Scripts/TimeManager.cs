// File taken from kart game Unity tutorial and edited by Thomas Mercurio to count up time

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool IsFinite { get; private set; }
    public float TotalTime { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsOver { get; private set; }
    public float CurrentTime { get; private set; }

    private bool raceStarted;

    public static Action<float> OnAdjustTime;
    public static Action<int, bool, GameMode> OnSetTime;

    private void Awake()
    {
        IsFinite = false;
        TimeRemaining = TotalTime;
        CurrentTime = -1;
    }


    void OnEnable()
    {
        OnAdjustTime += AdjustTime;
        OnSetTime += SetTime;
    }


    private void OnDisable()
    {
        OnAdjustTime -= AdjustTime;
        OnSetTime -= SetTime;
    }


    // Edited to count up instead of down
    private void AdjustTime(float delta)
    {
        TimeRemaining += delta;
    }


    private void SetTime(int time, bool isFinite, GameMode gameMode)
    {
        TotalTime = time;
        IsFinite = isFinite;
        TimeRemaining = TotalTime;
    }

    void Update()
    {
        if (!raceStarted) return;

        if (IsFinite && !IsOver)
            CurrentTime += Time.deltaTime;
    }

    public void StartRace()
    {
        raceStarted = true;
        IsFinite = true;
    }

    public void StopRace() {
        raceStarted = false;
        IsFinite = false;
    }
}
