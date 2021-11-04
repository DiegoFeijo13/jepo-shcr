using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string knockBackOnTag = "Player";

    private Rigidbody2D body;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(knockBackOnTag) && other.isTrigger)
        {
            body = other.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                Vector3 difference = body.transform.position - transform.position;
                difference = difference.normalized * thrust;

                body.DOMove(body.transform.position + difference, knockTime);
            }
        }
    }
}
