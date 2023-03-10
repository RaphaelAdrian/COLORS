using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(PhotonView))]
public class Trigger : MonoBehaviourPunCallbacks
{
    public Obstacle[] obstaclesToTrigger;
    public Sprite spriteToSwitch;
    protected bool isActivated;

    SpriteRenderer spriteRenderer;
    Sprite initSprite;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer)
            initSprite = spriteRenderer.sprite;
    }

    public virtual void ActivateButton(bool activate) {
        photonView.RPC("PunActivateButton", RpcTarget.AllViaServer, activate);
    }
    public void ActivateButtonOffline(bool activate) {
        PunActivateButton(activate);
    }

    [PunRPC]
    public void PunActivateButton(bool activate)
    {
        foreach(Obstacle obstacle in obstaclesToTrigger) {
            obstacle.Activate(activate);
        }

        isActivated = activate;
        SwitchSprites(activate);
        GetComponent<AudioComponent>()?.PlayOnNetwork(0);
    }

    private void SwitchSprites(bool activate)
    {
        if (spriteRenderer == null || spriteToSwitch == null)
            return;

        if (this.GetType() == typeof(ToggleSwitch)) {
            if (spriteRenderer.sprite == initSprite){
                spriteRenderer.sprite = spriteToSwitch;
            } else {
                spriteRenderer.sprite = initSprite;
            }
        } else {
            if (activate) {
                spriteRenderer.sprite = spriteToSwitch;
            } else {
                spriteRenderer.sprite = initSprite;
            }
        }
    }
}
