using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GameOverAd : MonoBehaviour
{
    private GameObject musicController;
    private InterstitialAd interstitial;

    private void Start()
    {
        musicController = GameObject.FindWithTag("MusicController");
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-8670378185122661/9052646750";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-8670378185122661/2132015187";
        #else
            string adUnitId = "unexpected_platform";    
        #endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;

        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        if (PlayerPrefs.GetInt("isMusicEnabled") == 1)
        {
            musicController.GetComponent<AudioSource>().UnPause();
        }
        interstitial.Destroy();
    }

    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        if (PlayerPrefs.GetInt("isMusicEnabled") == 1)
        {
            musicController.GetComponent<AudioSource>().Pause();
        }
        GameOver();
    }

    private void GameOver()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
}
