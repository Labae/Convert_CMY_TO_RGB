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
