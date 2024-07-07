using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public enum TypeAds
{
    Video,
    RewardedVideo
}

public class AdManager : MonoBehaviour
{  
    public Button adsButton;

    public TypeAds currentTypeAds;

    string typeAds = "";
    string gameID = "3236249";

    void Start()
    { 
        if (Application.platform == RuntimePlatform.Android)
        {
            //Advertisement.Initialize(gameID, false);
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        { 
            //Advertisement.Initialize(gameID, true);
        }

        switch (currentTypeAds)
        {
            case TypeAds.Video:
                typeAds = "video";
                adsButton.gameObject.SetActive(true);
                adsButton.onClick.AddListener(delegate { ShowAds(); });
                break;

            case TypeAds.RewardedVideo:
                typeAds = "rewardedVideo";
                adsButton.gameObject.SetActive(true);
                adsButton.onClick.AddListener(delegate { ShowAds(); });
                break;
        }
    }

    
    void ShowAds()
    {
        /*if (Advertisement.IsReady())
            Advertisement.Show(typeAds, new ShowOptions() { resultCallback = ResultAds });*/
    }
    /*
    void ResultAds(ShowResult resultAds)
    {
        /*if (resultAds == ShowResult.Finished)
        {
            
        }      
    }*/
}

