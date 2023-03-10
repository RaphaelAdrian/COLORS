using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : Obstacle
{
    public Vector2 moveToPosition = Vector2.zero;

    public bool isMovementCanStack;
    
    [Header("Animation")]
    public EaseMethod easeMethod = EaseMethod.EaseIn;
    public float duration = 1f;

    Vector3 initPosition;
    Vector3 targetPosition;

    // variables for animating
    int currentMove;
    float timeElapsed;
    float time;


    void Start() {
        initPosition = transform.localPosition;
        UpdateTargetPosition();
        // set z axis to avoid bugs
    }

    public override void Activate(bool isActivate)
    {
        base.Activate(isActivate);
        HandleMoveMultiplier(isActivate);
        UpdateTargetPosition(isActivate);
        timeElapsed = 0;
       

    }

    private void HandleMoveMultiplier(bool isActivate)
    {
        currentMove = isActivate ? currentMove + 1 : currentMove - 1;
    }

    private void UpdateTargetPosition(bool isActivate = false)
    {
        if (!isMovementCanStack) {
            if (isActivate)
                targetPosition = initPosition + (Vector3)moveToPosition;
            else 
                targetPosition = initPosition;
        } else {
                targetPosition = initPosition + (Vector3)moveToPosition * currentMove;
        }

    }

    void Update() {
        if (timeElapsed < duration) {
            time = Ease(timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, time);
        }    
    }


    private float Ease(float v)
    {
        switch (easeMethod) {
            case EaseMethod.EaseIn:
                return Easing.Quadratic.In(v);
            case EaseMethod.EaseOut:
                return Easing.Quadratic.Out(v);
            default:
                return Easing.Quadratic.InOut(v);   
        }
    }
}

public enum EaseMethod {
    EaseIn,
    EaseOut,
    EaseInOut
}
