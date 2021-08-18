using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
    public CharacterMovementModel CharacterMovementModel;

    static DialogBox Instance;

    TextMeshProUGUI _textBox;
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _textBox = GetComponentInChildren<TextMeshProUGUI>();

        Instance = this;
        Instance.DoHide();

    }
  
    public static void Show(string displayText)
    {
        Instance.DoShow(displayText);
    }

    public static void Hide()
    {
        Instance.DoHide();
    }

    public static bool IsVisible()
    {
        return Instance._textBox.enabled;
    }

    void DoShow(string displayText)
    {
        CharacterMovementModel.SetState(MovementState.frozen);
        StartCoroutine(FreezeTimeCo());
        _image.enabled = true;
        _textBox.enabled = true;
        _textBox.text = displayText;
    }

    void DoHide()
    {
        CharacterMovementModel.SetState(MovementState.idle);
        Time.timeScale = 1f;        

        _image.enabled = false;
        _textBox.enabled = false;
        _textBox.text = string.Empty;

        
    }

    IEnumerator FreezeTimeCo()
    {
        //Wait 1 frame
        yield return null;

        Time.timeScale = 0;
    }
}
