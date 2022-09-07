using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
    public Dialog CurrentDialog;
    TextMeshProUGUI _textBox;
    Image _image;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _textBox = GetComponentInChildren<TextMeshProUGUI>();
    }

    void FixedUpdate()
    {
        if (CurrentDialog.IsActive)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        _image.enabled = true;
        _textBox.enabled = true;
        _textBox.text = CurrentDialog.CurrentText;
    }

    private void Hide()
    {
        _image.enabled = false;
        _textBox.enabled = false;
        _textBox.text = string.Empty;
    }
}
