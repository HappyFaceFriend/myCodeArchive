using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    public void ShowNormalAd() //a video ad that user can skip
    {
        if(Advertisement.IsReady())
            Advertisement.Show();
    }
    public void ShowPictureAd()
    {
        if(Advertisement.IsReady("pictureZone"))
            Advertisement.Show("pictureZone");
    }
    public void ShowRewardAd()  
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            //Setting callback when ad closes
            ShowOptions options = new ShowOptions { resultCallback = RewardAdResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }
    public void RewardAdResult(ShowResult result)
    {
        //if the user saw full ad, result is 'Finished'.
        if (SceneManager.GetActiveScene().name == Defs.GameScene)
        {
            GameManager.Instance.AdDone(result == ShowResult.Finished);
        }
        if(SceneManager.GetActiveScene().name == Defs.GameOverScene)
        {
            OverSceneManager [] mgr = FindObjectsOfType<OverSceneManager>();
            mgr[0].AdDone(result == ShowResult.Finished);
        }
        
    }
}
