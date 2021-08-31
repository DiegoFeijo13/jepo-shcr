using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterMovementModel Movement;
    public CharacterInteractionModel Interaction;
    public CharacterInventoryModel Inventory;
    public CharacterMovementView MovementView;
    public PlayerHealth Health;
}
