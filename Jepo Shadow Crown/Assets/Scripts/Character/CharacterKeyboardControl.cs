using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterKeyboardControl : CharacterBaseControl
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
        UpdateSecondAttack();
    }

    void UpdateAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnAttackPressed();
        }

    }

    void UpdateSecondAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnSecondAttackPressed();
        }
    }

    void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnActionPressed();
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            newDirection.y = 1;
        if (Input.GetKey(KeyCode.S))
            newDirection.y = -1;
        if (Input.GetKey(KeyCode.A))
            newDirection.x = -1;
        if (Input.GetKey(KeyCode.D))
            newDirection.x = 1;

        SetDirection(newDirection);
    }
}
