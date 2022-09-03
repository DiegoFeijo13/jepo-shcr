using UnityEngine;

public class InteractableTorch : InteractableBase
{  
    [SerializeField] private bool isLit;
    [SerializeField] private Sprite litSprite;
    [SerializeField] private Sprite unlitSprite;


    public GameObject LightEffect;

    private void Awake()
    {
        LightEffect.SetActive(isLit);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (isLit && litSprite != null)
            spriteRenderer.sprite = litSprite;

        if (!isLit && unlitSprite != null)
            spriteRenderer.sprite = unlitSprite;
    }

    void Update()
    {
        UpdateInteract();
        
    }

    public override void InteractInternal()
    {
        isLit = !isLit;        
        LightEffect.SetActive(isLit);
        UpdateSprite();
    }
}
