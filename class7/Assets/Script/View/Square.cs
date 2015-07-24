using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 管理一个小方块
/// </summary>
public class Square : MonoBehaviour
{
    private Text _number = null;

    public string Number
    {
        set { _number.text = value.ToString(); }
    }

    void Awake()
    {
        _number = transform.GetComponentInChildren<Text>();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
