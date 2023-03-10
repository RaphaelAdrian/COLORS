using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RestartButton : MonoBehaviourPunCallbacks
{
    public GameObject restartPanel;
    public GameObject requestPanel;
    public GameObject waitingPanel;
    public void ShowPanel() {
        ShowWaitingPanel();
        photonView.RPC("PunRequestForRestart", RpcTarget.Others);
    }

    public void AcceptRequest() {
        GameManager.instance.levelManager.RestartLevel();
        restartPanel.SetActive(false);
    }

    public void DeclineRequest() {
        photonView.RPC("PunDeclineRequest", RpcTarget.All);
    }


    [PunRPC]
    private void PunRequestForRestart() {
        ShowRequestPanel();
    }

    [PunRPC]
    private void PunDeclineRequest() {
        HideAllPanels();
    }

    private void HideAllPanels()
    {
        restartPanel.SetActive(false);
        requestPanel.SetActive(false);
        waitingPanel.SetActive(false);
    }

    private void ShowRequestPanel()
    {
        restartPanel.SetActive(true);
        requestPanel.SetActive(true);
    }
    
    private void ShowWaitingPanel()
    {
        restartPanel.SetActive(true);
        waitingPanel.SetActive(true);
    }
}
