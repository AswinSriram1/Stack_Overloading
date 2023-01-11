using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    public static Ads instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance = null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void rewardedVideo()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show();
        }
    }
    public void videoAds()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show();
        }
    }
}