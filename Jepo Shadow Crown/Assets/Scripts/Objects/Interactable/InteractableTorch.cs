using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractableTorch : InteractableBase
{  
    public bool IsLit = false;
    
    public GameObject LightEffect;

    void Update()
    {
        UpdateInteract();
    }

    public override void InteractInternal()
    {
        bool active = LightEffect.activeSelf;
        LightEffect.SetActive(!active);
    }
}
