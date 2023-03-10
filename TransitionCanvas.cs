using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCanvas : MonoBehaviour
{
    public GameObject transitionIn;
    public GameObject transitionOut;
    public float transitionTime = 1f;
    public void TransitionIn() {
        StartCoroutine(StartTransitionIn());
    }

    private IEnumerator StartTransitionIn()
    {
        transitionIn.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        transitionIn.SetActive(false);
    }

    public void TransitionOut() {
        transitionOut.SetActive(true);
    }
}
