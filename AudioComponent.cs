using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviourPunCallbacks
{
    public AudioClip[] audioClips;
    [Header("Optional")]
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int audioClipIndex = -1) {
        if (audioClipIndex == -1) {
            audioSource.Play();
        } else {
            audioSource.PlayOneShot(audioClips[audioClipIndex]);
        }
    }
    public void PlayOnNetwork(int audioClipIndex = -1) {
        photonView.RPC("PunPlaySound", RpcTarget.All, audioClipIndex);
    }

    [PunRPC]
    public void PunPlaySound(int audioClipIndex) {                                                              
        if (audioClipIndex == -1) {
            audioSource.Play();
        } else {
            audioSource.PlayOneShot(audioClips[audioClipIndex]);
        }
    }

}
