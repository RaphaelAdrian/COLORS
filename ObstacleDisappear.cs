using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDisappear : Obstacle
{
    public enum Mode {
        appear,
        disappear
    }

    public Mode mode = Mode.disappear;

    void Start() {
        if (mode == Mode.appear)
            gameObject.SetActive(false);
    }
    public override void Activate(bool isActivate)
    {
        base.Activate(isActivate);
        
        if (mode ==  Mode.disappear) {
            gameObject.SetActive(!isActivate);
        } else {
            gameObject.SetActive(isActivate);
        }
    }

}
