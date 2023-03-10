using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourPunCallbacks
{
    public TransitionCanvas transitionCanvas;
    bool isGameFinished;

    void Start() {
        transitionCanvas = Instantiate(transitionCanvas);
        transitionCanvas.TransitionIn();
    }
    public void GoToNextLevel() {
        if (!isGameFinished && PhotonNetwork.IsMasterClient) {
            photonView.RPC("PunNextLevel", RpcTarget.AllViaServer);
            isGameFinished = true;
        }
    }
    
    public void RestartLevel() {
        if (!isGameFinished) {
            photonView.RPC("PunRestartGame", RpcTarget.AllViaServer);
            isGameFinished = true;
        }
    }
    
    [PunRPC]
    public void PunRestartGame() {
        StartCoroutine(RestartGame());
    }

    [PunRPC]
    public void PunNextLevel() {
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        // disable playermovement first
        GameManager.instance.player.GetComponent<PlayerMovement>().enabled = false;
        GameManager.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        yield return new WaitForSeconds(4f);
        transitionCanvas.TransitionOut();
        yield return new WaitForSeconds(1f);
        if (PhotonNetwork.IsMasterClient) {
            int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            buildIndex = buildIndex < SceneManager.sceneCountInBuildSettings ? buildIndex : 0;
            PhotonNetwork.LoadLevel(buildIndex);
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        transitionCanvas.TransitionOut();
        yield return new WaitForSeconds(1f);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex);
    }
}
