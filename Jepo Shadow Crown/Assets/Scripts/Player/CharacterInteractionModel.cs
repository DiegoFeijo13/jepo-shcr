using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionModel : MonoBehaviour
{
    private Collider2D _collider;
    private PlayerControl playercontrol;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        playercontrol = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract()
    {
        InteractableBase interactable = FindUsableInteractable();
        if (interactable == null)
            return;
        
        interactable.OnInteract();
        
    }

    public Collider2D[] GetCloseColliders()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        return Physics2D.OverlapAreaAll(
            (Vector2)transform.position + boxCollider.offset + boxCollider.size * 0.6f,
            (Vector2)transform.position + boxCollider.offset - boxCollider.size * 0.6f);
    }

    InteractableBase FindUsableInteractable()
    {
        Collider2D[] closeColliders = GetCloseColliders();
        InteractableBase closestInteractable = null;
        float angleToClosestInteractable = Mathf.Infinity;
        for (int i = 0; i < closeColliders.Length; i++)
        {
            InteractableBase colliderInteractable = closeColliders[i].GetComponent<InteractableBase>();

            if (colliderInteractable == null)
                continue;

            Vector3 directionToInteractable = closeColliders[i].transform.position - transform.position;
            
            float angleToInteractable = Vector3.Angle(playercontrol.GetFacingDirection(), directionToInteractable);

            //Debug.Log("Angle" + angleToInteractable);
            //if (angleToInteractable < 40)
            //{
                if (angleToInteractable < angleToClosestInteractable)
                {
                    closestInteractable = colliderInteractable;
                    angleToClosestInteractable = angleToInteractable;
                }
            //}

        }

        return closestInteractable;
    }
}
