using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class show how many are mixed color.
/// 이 클래스는 색깔이 추가된(섞인) 수를 보여준다.
/// </summary>
public class ColorUI : MonoBehaviour
{
    // Color Manager
    [SerializeField] private ColorManager _colorManager = null;
    // 색상을 더하는 버튼
    [SerializeField] private Button _colorAddButton = null;
    // 색상을 더한 수를 보여주는 텍스트
    [SerializeField] private Text _colorCountText = null;
    // 색상을 빼는 버튼
    [SerializeField] private Button _colorSubstractionButton = null;
    private int _colorCount = 0;

    public ColorType CurrentColorType;
    private int colorTypeIndex;

    private void Start()
    {
        Initialize();

        colorTypeIndex = (int)CurrentColorType;
    }

    private void Initialize()
    {
        if (_colorAddButton == null)
        {
            _colorAddButton = GetComponent<Button>();
            if (_colorAddButton == null)
            {
                Debug.LogError(gameObject.name + " Color add button is null");
                return;
            }

            _colorAddButton.onClick.AddListener(AddColor);
        }

        if (_colorCountText == null)
        {
            _colorCountText = GetComponentInChildren<Text>();
            if (_colorCountText == null)
            {
                Debug.LogError(gameObject.name + " Color count text is null");
                return;
            }
        }

        if (_colorSubstractionButton == null)
        {
            _colorSubstractionButton = transform.GetChild(1).GetComponentInChildren<Button>();
            if (_colorSubstractionButton == null)
            {
                Debug.LogError(gameObject.name + " Color minus button is null");
                return;
            }

            _colorSubstractionButton.onClick.AddListener(SubstractionColor);
            _colorSubstractionButton.gameObject.SetActive(false);
        }

        if(_colorManager == null)
        {
            _colorManager = FindObjectOfType<ColorManager>();
        }
    }

    public void AddColor()
    {
        _colorCount++;
        _colorCountText.text = _colorCount.ToString();
        _colorManager.AddMaterialColor(colorTypeIndex);

        if (!_colorSubstractionButton.gameObject.activeSelf)
        {
            _colorSubstractionButton.gameObject.SetActive(true);
        }
    }

    public void SubstractionColor()
    {
        _colorCount--;
        _colorCountText.text = _colorCount.ToString();
        _colorManager.SubstractionColor(colorTypeIndex);

        if (_colorCount == 0)
        {
            _colorSubstractionButton.gameObject.SetActive(false);
        }
    }

    public void ResetCount()
    {
        _colorCount = 0;
        _colorCountText.text = _colorCount.ToString();
        _colorManager.ResetMaterialColor();
        if (_colorSubstractionButton.gameObject.activeSelf)
        {
            _colorSubstractionButton.gameObject.SetActive(false);
        }
    }
}
