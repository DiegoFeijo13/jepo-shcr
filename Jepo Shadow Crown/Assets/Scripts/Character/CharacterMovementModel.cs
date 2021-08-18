using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : BaseMovementModel
{
 

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _currentState = MovementState.idle;
    }

    private void Update()
    {
        UpdatePushTime();
    }


    void Start()
    {
        SetDirection(new Vector2(0, -1));
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    protected override void UpdateMovement()
    {
        base.UpdateMovement();
    }

    internal void SetState(MovementState state)
    {
        _currentState = state;
    }

    internal MovementState GetState()
    {
        return _currentState;
    }
}
