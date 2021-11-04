using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InteractableTorch : InteractableBase
{  
    public bool IsLit;
    
    public GameObject LightEffect;

    private void Awake()
    {
        LightEffect.SetActive(IsLit);
    }

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
