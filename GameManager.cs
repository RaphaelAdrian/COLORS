using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static GameManager instance = null;

    public GameObject spawnPointBlue;
    public GameObject spawnPointPink;
    internal Player player;
    bool isOfflineMode;

    internal LevelManager levelManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
            
        HandleOfflineMode();
    }

    void Start()
    {

        levelManager = GetComponent<LevelManager>();

        if (!isOfflineMode) {
            if (PhotonNetwork.IsMasterClient) 
                PlayAsGreen();
            else 
                PlayAsPink();
        } else {
            PlayAsPink();
            PlayAsGreen();
        }
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            levelManager.GoToNextLevel();
        }
    }
    public void PlayAsPink()
    {
        GameObject playerObject = PhotonNetwork.Instantiate("Player_Pink", spawnPointPink.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
    }

    public void PlayAsGreen()
    {
        GameObject playerObject = PhotonNetwork.Instantiate("Player_Green", spawnPointBlue.transform.position, Quaternion.identity);
        player = playerObject.GetComponent<Player>();
    }
    
    private void HandleOfflineMode()
    {
        isOfflineMode = PhotonNetwork.PhotonServerSettings.StartInOfflineMode;
        PhotonNetwork.OfflineMode = isOfflineMode;
        if (isOfflineMode && !PhotonNetwork.InRoom)
            PhotonNetwork.CreateRoom("");
        else {
            if (!PhotonNetwork.IsConnectedAndReady)
               SceneManager.LoadScene(0);
        }
    }

}
