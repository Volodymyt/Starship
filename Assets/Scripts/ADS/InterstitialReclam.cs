using GoogleMobileAds.Api;
using UnityEngine;

public class InterstitialReclam : MonoBehaviour
{
    private InterstitialAd _interstitial;
    private const string adUnitID = "ca-app-pub-3940256099942544/1033173712";

    private void OnEnable()
    {
        this._interstitial = new InterstitialAd(adUnitID);
        AdRequest request = new AdRequest.Builder().Build();
        this._interstitial.LoadAd(request);
    }

    public void Show()
    {
        if (this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }
    }
}
