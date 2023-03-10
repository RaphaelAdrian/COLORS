using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(PhotonView))]
public class ToggleSwitch : Trigger
{
    KeyCode toggleInput = KeyCode.E;
    bool isOnTrigger;

    void Update() {
        if (obstaclesToTrigger.Length == 0)
            return;
            
        if (Input.GetKeyDown(toggleInput) && isOnTrigger) {
            isActivated = !obstaclesToTrigger[0].isActivated;
            ActivateButton(isActivated);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        // if you are the player, then enable 
        if (player?.playerType == GameManager.instance.player.playerType) {
            isOnTrigger = true;
        }

        // handle offline mode
        if (PhotonNetwork.OfflineMode && player) {
            isOnTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        // if you are the player, then enable 
        if (player?.playerType == GameManager.instance.player.playerType) {
            isOnTrigger = false;
        }
        
        // handle offline mode
        if (PhotonNetwork.OfflineMode && player) {
            isOnTrigger = false;
        }
    }
}