using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    internal bool isActivated;
    // Start is called before the first frame update
    public virtual void Activate(bool isActivate) {
        isActivated = isActivate;
    }
}
