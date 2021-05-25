using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    public virtual void OnInteract()
    {
        Debug.Log("OnInteract not implemented");
    }



}
