using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressFromObject : ButtonPress
{
    public GameObject triggeringObject;

    public override void OnButtonPressed(Collider2D other)
    {
        if (triggeringObject == other.gameObject) {
            base.OnButtonPressed(other);
        }
    }
}
