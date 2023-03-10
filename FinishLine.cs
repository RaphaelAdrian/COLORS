using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine; 

public class FinishLine : MonoBehaviourPunCallbacks
{
    public PlayerType doorColor = PlayerType.Green;
    public GameObject enabledIndicator;
    FinishLineManager finishLineManager;

    void Start() {
        enabledIndicator.SetActive(false);
        finishLineManager = GameManager.instance.GetComponent<FinishLineManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player?.playerType == doorColor){
            photonView.RPC("PunSendFinish", RpcTarget.All, (byte)doorColor, true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player?.playerType == doorColor){
            photonView.RPC("PunSendFinish", RpcTarget.All, (byte)doorColor, false);
        }
    }
    [PunRPC]
    public void PunSendFinish(byte bytePlayerType, bool isEnter)
    {
        PlayerType playerType = (PlayerType)bytePlayerType;
        finishLineManager.UpdateStatus(playerType, isEnter);
        enabledIndicator.SetActive(isEnter);

        if (isEnter)
            GetComponent<AudioComponent>()?.PlayOnNetwork(0);
    }
}
