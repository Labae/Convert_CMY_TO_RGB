using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // 색상이 변할 대상.
    [SerializeField] private GameObject _targetObject = null;
    // 모든 색깔이 더해진 횟수.
    [SerializeField] private int _addedColorCount = 0;

    // color type count.
    // ColorType에 맞는 색깔이 추가된 횟수.
    private Dictionary<ColorType, int> _colorTypeCountDictionary = new Dictionary<ColorType, int>();

    // 색상이 변할 대상의 Material.
    private Material _targetObjectMaterial = null;

    private void Start()
    {
        if (_targetObject == null)
        {
            Debug.LogError("Target Object is null.");
            Debug.Break();
            return;
        }

        _targetObjectMaterial = _targetObject.GetComponent<Renderer>().material;
    }

    // TODO
    // fix param type int to enum
    // can use in onclick event on button.
    public void AddMaterialColor(int type)
    {
        // add ColorType dictionary
        ColorType addedColorType = (ColorType)type;
        if (!_colorTypeCountDictionary.ContainsKey(addedColorType))
        {
            _colorTypeCountDictionary.Add(addedColorType, 0);
        }
        _colorTypeCountDictionary[addedColorType]++;

        Color rgbOriginColor = Color.white;
        _addedColorCount++;

        CMYK_Color originCMYKColor = new CMYK_Color(rgbOriginColor);
        CMYK_Color mixedColor = originCMYKColor;

        // Add color according to ratio.
        // 색을 개수에 따른 비율로 더해준다.
        for (int i = 0; i < _colorTypeCountDictionary.Count; i++)
        {
            ColorType colorType = (ColorType)i;
            if(_colorTypeCountDictionary.ContainsKey(colorType))
            {
                CMYK_Color addedColor = new CMYK_Color(GetRGBColor(colorType));
                float multiplationValue = (float)_colorTypeCountDictionary[colorType] / (float)_addedColorCount;
                addedColor = addedColor * multiplationValue;

                mixedColor += addedColor;
            }
        }

        _targetObjectMaterial.color = mixedColor.GetRGBColor();
    }

    /// <summary>
    /// Reset target Object Material Color to use Button.
    /// </summary>
    public void ResetMaterialColor()
    {
        _targetObjectMaterial.color = Color.white;
        _addedColorCount = 0;

        int colorTypeLast = (int)ColorType.LAST;
        for (int i = 0; i < colorTypeLast; i++)
        {
            ColorType type = (ColorType)i;
            if (_colorTypeCountDictionary.ContainsKey(type))
            {
                _colorTypeCountDictionary[type] = 0;
            }
        }
    }

    // Get Color to use Color Type enum
    private Color GetRGBColor(ColorType type)
    {
        Color color = Color.clear;
        switch (type)
        {
            case ColorType.RED:
                color = Color.red;
                break;
            case ColorType.ORANGE:
                color = new Color(255.0f, 165.0f, 0.0f).DemcialToOne();
                break;
            case ColorType.YELLOW:
                color = Color.yellow;
                break;
            case ColorType.GREEN:
                color = Color.green;
                break;
            case ColorType.BLUE:
                color = Color.blue;
                break;
            case ColorType.PURPLE:
                color = new Color(255.0f, 0.0f, 255.0f).DemcialToOne();
                break;
            case ColorType.BLACK:
                color = Color.black;
                break;
        }
        return color;
    }
}

public enum ColorType
{
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    BLUE,
    PURPLE,
    BLACK,
    LAST
}

/// <summary>
/// CMYK Color class.
/// </summary>
public class CMYK_Color
{
    public float C, M, Y, K;

    private CMYK_Color(float c, float m, float y, float k)
    {
        C = c;
        M = m;
        Y = y;
        K = k;
    }

    /// <summary>
    /// Convert RGB Color to CMYK Color
    /// </summary>
    /// <param name="color"></param>
    public CMYK_Color(Color color)
    {
        float deltaR = color.r;
        float deltaG = color.g;
        float deltaB = color.b;


        K = 1 - Mathf.Max(deltaR, deltaG, deltaB);
        if (1 - K == 0)
        {
            C = M = Y = 0.0f;
            return;
        }

        C = (1 - deltaR - K) / (1 - K);
        M = (1 - deltaG - K) / (1 - K);
        Y = (1 - deltaB - K) / (1 - K);
    }

    /// <summary>
    /// Convert CMYK Color To RGB Color
    /// </summary>
    /// <returns></returns>
    public Color GetRGBColor()
    {
        float r = (1 - C) * (1 - K);
        float g = (1 - M) * (1 - K);
        float b = (1 - Y) * (1 - K);

        return new Color(r, g, b);
    }

    /// <summary>
    /// Print CMYK Element.
    /// </summary>
    public void Print()
    {
        Debug.Log("C : " + C.ToString() + "  M : " + M.ToString()
                        + "  Y : " + Y.ToString() + "  K : " + K.ToString());
    }

    public static CMYK_Color operator +(CMYK_Color color1, CMYK_Color color2)
    {
        color1.Print();
        color2.Print();
        float c = Mathf.Clamp01(color1.C + color2.C);
        float m = Mathf.Clamp01(color1.M + color2.M);
        float y = Mathf.Clamp01(color1.Y + color2.Y);
        float k = Mathf.Clamp01(color1.K + color2.K);

        CMYK_Color returnColor = new CMYK_Color(c, m, y, k);
        returnColor.Print();
        return returnColor;
    }

    public static CMYK_Color operator *(CMYK_Color color, float value)
    {
        if(value < 0.0f)
        {
            Debug.LogError("ERROR CMYK MULTIPLATION VALUE IS LESS THAN ZERO.");
            return new CMYK_Color(Color.clear);
        }

        return new CMYK_Color(color.C * value, color.M * value, color.Y * value, color.K * value);
    }
}
