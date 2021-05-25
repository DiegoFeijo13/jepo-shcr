using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationListener : MonoBehaviour
{
    public CharacterMovementModel MovementModel;
    public CharacterMovementView MovementView;

    // Start is called before the first frame update
    public void OnAttackStart()
    {
        Debug.Log("Attack start");
        if (MovementModel != null)        
            MovementModel.OnAttackStart();


        if (MovementView != null)
            MovementView.OnAttackStart();
    }

    public void OnAttackStop()
    {
        Debug.Log("Attack stop");
        if (MovementModel != null)
            MovementModel.OnAttackStop();


        if (MovementView != null)
            MovementView.OnAttackStop();
    }
}
