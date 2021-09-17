using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : BaseMovementModel
{    
    private Vector2 _movement;
    private PlayerView _view;
    private CharacterInteractionModel _interactionModel;

    public Vector2 BodyPos => _body.position;

    private void Awake()
    {
        _view = GetComponent<PlayerView>();
        _interactionModel = GetComponent<CharacterInteractionModel>();        
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");        

        UpdateAction();
    }

    private void FixedUpdate()
    {
        if (_movement == Vector2.zero)        
            SetDirection(Vector2.zero);        
        else                   
            SetDirection(_movement);

        if(CurrentState != MovementState.staggering)
            UpdateMovement();
    }

    void UpdateAction()
    {
        if (Input.GetButtonDown("Action"))
        {            
            OnActionPressed();
        }
    }

    protected void OnActionPressed()
    {
        if (_interactionModel == null)
            return;

        _interactionModel.OnInteract();
    }

}
