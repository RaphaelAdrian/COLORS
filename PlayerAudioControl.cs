using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{
    PlayerMovement playerMovement;
    Player player;
    AudioComponent audioComponent;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<Player>();
        audioComponent = GetComponent<AudioComponent>();

        playerMovement.OnJump += OnJump;
        player.OnDie += OnDie;
    }

    private void OnDie()
    {
        audioComponent.PlayOnNetwork(2);
    }

    public void OnJump() {
        audioComponent.PlayOnNetwork(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
