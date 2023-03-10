using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OfflinePlayerMovement : MonoBehaviourPunCallbacks
{
     [Header("Offline Controls")]
    public KeyCode jumpKeyPlayer2 = KeyCode.UpArrow;
    PlayerMovement playerMovement;

    Player player;
    // Start is called before the first frame update
    void Awake()
    {
        if (PhotonNetwork.OfflineMode) {
            player = GetComponent<Player>();
            playerMovement = GetComponent<PlayerMovement>();
            ChangeMovementInput();
        } else {
            this.enabled = false;
        }
    }

    private void ChangeMovementInput()
    {
        if (player.playerType == PlayerType.Pink) {
            playerMovement.horizontalInput = "Horizontal2";
            playerMovement.verticalInput = "Vertical2";
            playerMovement.jumpKey = jumpKeyPlayer2;
            player.GetComponentInChildren<AudioListener>().enabled = false;
        }
    }
}
