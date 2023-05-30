using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Point : MonoBehaviour
{
    [SerializeField] TMP_Text block;
    public bool isBonus = false;
    public bool isTrap = false;
    void Start()
    {
        block = GetComponentInChildren<TMP_Text>();
        DisplayBlockName();
    }
    void DisplayBlockName()
    {
        block.text = gameObject.name;
    }
}
