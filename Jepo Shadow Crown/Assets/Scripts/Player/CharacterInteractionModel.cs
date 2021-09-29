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
        
        
        
    }


}
