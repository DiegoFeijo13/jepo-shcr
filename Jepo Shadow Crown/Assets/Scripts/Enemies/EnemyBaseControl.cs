using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseControl : MonoBehaviour
{
    protected BaseMovementModel _movementModel;    
    private BaseMovementView _movementView;

    private void Awake()
    {
        _movementModel = GetComponent<BaseMovementModel>();        
        _movementView = GetComponent<BaseMovementView>();
    }

    protected void SetDirection(Vector2 direction)
    {
        if (_movementModel == null)
            return;

        _movementModel.SetDirection(direction);
    }
}
