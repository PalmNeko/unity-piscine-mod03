using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[UxmlElement]
public partial class PopupElement : VisualElement
{
    [UxmlAttribute]
    public string message;
    [UxmlAttribute]
    public string okText;
    [UxmlAttribute]
    public string cancelText;

    public Label messageLabel;
    public Button OKButton;
    public Button cancelButton;

    public PopupElement() {}
    public PopupElement(string message, string okText, string cancelText)
    {
        this.message = message;
        this.okText = okText;
        this.cancelText = cancelText;
        Init();
    }

    private VisualTreeAsset _uxml;
    [UxmlAttribute]
    [SerializeField]
    [Tooltip("PopupElement.uxmlが設定されることを想定しています。")]
    private VisualTreeAsset uxml
    {
        get => _uxml;
        set
        {
            _uxml = value;
            Init();
        }
    }

    public void Init()
    {
        if (_uxml == null)
            return;
        Clear();
        _uxml.CloneTree(this);

        messageLabel = this.Q<Label>("Message");
        OKButton = this.Q<Button>("OK");
        cancelButton = this.Q<Button>("Cancel");

        if (message != null)
            messageLabel.text = message;
        if (okText != null)
            OKButton.text = okText;
        if (cancelText != null)
            cancelButton.text = cancelText;

        style.flexGrow = new StyleFloat(1.0f);
        style.position = Position.Absolute;
        style.left = 0f;
        style.top = 0f;
        style.width = new StyleLength(new Length(100f, LengthUnit.Percent));
        style.height = new StyleLength(new Length(100f, LengthUnit.Percent));
    }
}
