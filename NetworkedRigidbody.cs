using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class NetworkedRigidbody : MonoBehaviourPunCallbacks
{
    void OnCollisionEnter2D(Collision2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
            photonView.TransferOwnership(player.photonView.Owner);
    }
}
