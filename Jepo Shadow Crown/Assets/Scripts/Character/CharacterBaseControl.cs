using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseControl : MonoBehaviour
{
    protected CharacterMovementModel _movementModel;
    private CharacterInteractionModel _interactionModel;
    private CharacterMovementView _movementView;

    private void Awake()
    {
        _movementModel = GetComponent<CharacterMovementModel>();
        _interactionModel = GetComponent<CharacterInteractionModel>();
        _movementView = GetComponent<CharacterMovementView>();
    }

    protected void SetDirection(Vector2 direction)
    {
        if (_movementModel == null)
            return;

        _movementModel.SetDirection(direction);
    }

    protected void OnActionPressed()
    {
        if (_interactionModel == null)
            return;

        _interactionModel.OnInteract();
    }

    protected void OnAttackPressed()
    {
        if (_movementModel == null || _movementView == null)
            return;

        if (!_movementModel.CanAttack())
            return;
        
        _movementView.DoAttack();
    }

    protected void OnSecondAttackPressed()
    {
        if (_movementModel == null || _movementView == null)
            return;

        if (!_movementModel.CanAttack())
            return;

        _movementView.DoSecondAttack();
    }
}
