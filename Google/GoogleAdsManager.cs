using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public class GoogleAdsManager : MonoBehaviour
{
    public AdType adType = AdType.CoinX2;

    public UIManager uIManager;
    public GameManager gameManager;
    public ShopManager shopManager;

    string adUnitId;

    private RewardedAd rewardedAd;

    PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;
    }

    public void Start()
    {
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-6754544778509872/8238444835";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-6754544778509872/7165886378";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    public void Show(int number)
    {
        adType = AdType.CoinX2;
        adType += number;


        if(playerDataBase.RemoveAd)
        {
            switch(number)
            {
                case 0:
                    uIManager.SuccessWatchAd();

                    break;
                case 1:
                    gameManager.SuccessWatchAd();

                    break;
                case 2:
                    shopManager.SuccessWatchAd();

                    break;
            }
            return;
        }

        if (!GameStateManager.instance.WatchAd) return;

        StartCoroutine(ShowRewardAd());
    }

    IEnumerator ShowRewardAd()
    {
        Debug.Log("Show Reward Ad");

        while (!rewardedAd.IsLoaded())
        {
            yield return null;
        }
        rewardedAd.Show();
    }

    public void ReloadAd()
    {
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-6754544778509872/8238444835";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-6754544778509872/7165886378";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        ReloadAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        switch (adType)
        {
            case AdType.CoinX2:
                uIManager.SuccessWatchAd();
                break;
            case AdType.TryCount:
                gameManager.SuccessWatchAd();
                break;
            case AdType.ShopWatchAd:
                shopManager.SuccessWatchAd();
                break;
        }
    }
}