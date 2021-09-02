using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Knockback", other);

            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                BaseMovementModel baseMovementModel = other.gameObject.GetComponent<BaseMovementModel>();

                Debug.Log("BaseMovementModel", baseMovementModel);
                if (baseMovementModel != null)
                {
                    baseMovementModel.CurrentState = MovementState.staggering;
                    baseMovementModel.Knock(knockTime);
                }
            }
        }
    }
}
