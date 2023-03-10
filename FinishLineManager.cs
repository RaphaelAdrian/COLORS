using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FinishLineManager : MonoBehaviourPunCallbacks
{
    public bool isGreenFinish;
    public bool isPinkFinish;

    public void UpdateStatus(PlayerType playerType, bool isEnter)
    {
        if (playerType == PlayerType.Green)
            isGreenFinish = isEnter;
        else
            isPinkFinish = isEnter;

        // if both is finished, then go to next level
        if (isGreenFinish && isPinkFinish) {
            GameManager.instance.levelManager.GoToNextLevel();
        }
    }
}
