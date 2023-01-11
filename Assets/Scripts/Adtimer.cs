﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Adtimer : MonoBehaviour
{
    [SerializeField] int AdTime = 3;
    //Ads ads;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(callAD());
    }

    IEnumerator callAD()
    {
        yield return new WaitForSeconds(AdTime);
        //Ads.instance.videoAds();
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show();
        }
    }
}
