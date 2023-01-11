﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsForLose : MonoBehaviour
{
    public static AdsForLose instance;       //AdsForLose.instance.CalculatingScene(); (<-use this to call)
    [SerializeField] private int timeToLose = 3;
    private const string gamesInARow = "gamesInARow";

    private void Awake()
    {
        instance = this;
    }
    public void CalculatingScene()
    {
        int gameLost = PlayerPrefs.GetInt(gamesInARow) + 1;
        PlayerPrefs.SetInt(gamesInARow, gameLost);
        if (gameLost >= timeToLose)
        {
            PlayerPrefs.SetInt(gamesInARow, 0);
            videoAds();
        }
    }
    private void videoAds()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }

}
