using CustomText;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorByType : MonoBehaviour
{
    [SerializeField] private Custom_ColorStyle _type;
    [SerializeField] private Image _image;
    private bool _isSubcribed = false;
    private Custom_ColorStyle _selectedTextColor = Custom_ColorStyle.Default;

    public Custom_ColorStyle Color => _type;

    private void Awake() => ApplyColorSetting();

    private void ApplyColorSetting()
    {
        _selectedTextColor = _type;
        if (ColorSettings.Instance == null) return;
        ColorParams textColor = ColorSettings.Instance.Colors.Find(t => t.TextColorType.Equals(_type));
        if (textColor != null) _image.color = textColor.Color;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        Change();
    }

#endif

    private void Change()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            Debug.LogError("ProceduralImage not found in " + gameObject.name);
            return;
        }

        if (!_isSubcribed)
        {
            ColorSettings.Instance.OnColorStyleChanged += ApplyColorSetting;
            _isSubcribed = true;
        }

        if (_type != _selectedTextColor) ApplyColorSetting();
    }

    private void OnDestroy()
    {
        ColorSettings.Instance.OnColorStyleChanged -= ApplyColorSetting;
    }

    public void ChangeColor(Custom_ColorStyle type)
    {
        _selectedTextColor = type;
        if (ColorSettings.Instance == null) return;
        ColorParams textColor = ColorSettings.Instance.Colors.Find(t => t.TextColorType.Equals(type));
        if (textColor != null)
        {
            _type = _selectedTextColor;
            Change();
            _image.color = textColor.Color;
        }

    }
}