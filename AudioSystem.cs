using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance = null;
    public AudioClip clickSound;
    AudioSource audioSource;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
