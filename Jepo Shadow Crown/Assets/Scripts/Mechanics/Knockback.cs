using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public string knockBackOn = "Player";

    private Rigidbody2D _body;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(knockBackOn))
        {
            _body = other.GetComponent<Rigidbody2D>();
            BaseMovementModel baseMovementModel = other.gameObject.GetComponent<BaseMovementModel>();
            if (_body != null && baseMovementModel != null)
            {
                Vector2 difference = _body.transform.position - transform.position;
                difference = difference.normalized * thrust;
                _body.AddForce(difference, ForceMode2D.Impulse);

                baseMovementModel.CurrentState = MovementState.staggering;
                StartCoroutine(KnockCo(knockTime, baseMovementModel));
            }

        }
    }

    private IEnumerator KnockCo(float knockTime, BaseMovementModel baseMovementModel)
    {
        if (_body != null)
        {
            yield return new WaitForSeconds(knockTime);
            _body.velocity = Vector2.zero;
            baseMovementModel.CurrentState = MovementState.idle;
        }
    }
}
