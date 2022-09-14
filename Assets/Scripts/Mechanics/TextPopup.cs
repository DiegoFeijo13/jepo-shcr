using Assets.Scripts.General;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class TextPopup : MonoBehaviour
{
    private TextMeshPro tmPro;
    private float disappearTimer;
    private Color textColor;

    public static TextPopup ShowHeal(int health, Vector3 pos)
    {
        return Create(health.ToString(), UIAttributes.HealColor, pos, UIAttributes.FontSizeMedium);
    }

    public static TextPopup ShowDamage(int damage, Vector3 pos, bool isCritical)
    {
        Color color = UIAttributes.NormalDamageColor;
        int fontSize = UIAttributes.FontSizeMedium;
        if (isCritical)
        {
            color = UIAttributes.CriticalDamageColor;
            fontSize = UIAttributes.FontSizeLarge;
        }

        return Create(damage.ToString(), color, pos, fontSize);
    }

    public static TextPopup Create(string text, Color color, Vector3 pos, int fontSize)
    {
        Transform textPopupTransform = Instantiate(GameAssets.Instance.TextPopup, pos, Quaternion.identity);
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.Setup(text, color, fontSize);

        return textPopup;
    }

    private void Awake()
    {
        tmPro = transform.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        float moveYSpeed = 1f;

        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime; 
            tmPro.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Setup(string text, Color color, int fontSize)
    {
        tmPro.SetText(text);
        tmPro.color = color;
        tmPro.fontSize = fontSize;
        textColor = color;
        disappearTimer = .5f;

    }
}
