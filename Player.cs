using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public delegate void CharacterEvent();
    public CharacterEvent OnDie;
    
    public PlayerType playerType = PlayerType.Green;
    // Start is called before the first frame update
    void Start()
    {        
        // Disable unnecessary components
        if (!photonView.IsMine) {
            GetComponent<PlayerMovement>().enabled = false;
            Destroy(GetComponent<Rigidbody2D>());
            GetComponentInChildren<AudioListener>().enabled = false;
        } else {
            Camera.main.GetComponent<CameraFollow>().targetObject = this.transform;
        }

        OnDie += Die;
    }

    public void Update() {
        if (!photonView.IsMine) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            GameManager.instance.levelManager.RestartLevel();
        }
    }

    public void Die() {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Animator>().SetBool("isDead", true);
        GameManager.instance.levelManager.RestartLevel();
    }
}


public enum PlayerType {
    Green,
    Pink   
}
