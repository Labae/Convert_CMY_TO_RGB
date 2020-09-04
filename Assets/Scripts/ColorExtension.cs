using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension
{
    
    /// <summary>
    /// Change Color element max value 255 to 1.
    /// 컬러 값의 최대치를 255에서 1로 변경한다.
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color DemcialToOne(this Color color)
    {
        return new Color(color.r / 255.0f, color.g / 255.0f, color.b / 255.0f, color.a / 255.0f);
    }
}

/// <summary>
/// Color Util class.
/// </summary>
public class ColorUtils 
{
    // Get Color to use Color Type enum
    public static Color GetRGBColor(ColorType type)
    {
        Color color = Color.clear;
        switch (type)
        {
            case ColorType.RED:
                color = Color.red;
                break;
            case ColorType.ORANGE:
                color = new Color(255.0f, 165.0f, 0.0f, 255.0f).DemcialToOne();
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
                color = new Color(255.0f, 0.0f, 255.0f, 255.0f).DemcialToOne();
                break;
            case ColorType.BLACK:
                color = Color.black;
                break;
        }
        return color;
    }
}
