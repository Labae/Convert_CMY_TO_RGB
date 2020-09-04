using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUIGenerator : MonoBehaviour
{
    [Tooltip("Color UI Prefab")]
    [SerializeField] private GameObject _colorUIPrefab;

    [Tooltip("Color Reset Button")]
    [SerializeField] private Button _colorResetButton;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_colorResetButton == null)
        {
            Debug.LogError("Color reset button is null");
            return;
        }

        if (_colorUIPrefab == null)
        {
            Debug.LogError($"ColorUIGenerator : Color ui prefab is null");
            return;
        }

        int colorLastIndex = (int)ColorType.LAST;
        for (int i = 0; i < colorLastIndex; i++)
        {
            ColorType colorType = (ColorType)i;
            GameObject uiObject = Instantiate(_colorUIPrefab, transform);
            uiObject.name = colorType.ToString();
            uiObject.GetComponent<Image>().color = ColorUtils.GetRGBColor(colorType);

            ColorUI colorUI = uiObject.GetComponent<ColorUI>();
            colorUI.CurrentColorType = (ColorType)i;
            _colorResetButton.onClick.AddListener(() => colorUI.ResetCount());
        }
    }
}
