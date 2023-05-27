using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Point : MonoBehaviour
{
    [SerializeField] TMP_Text block;

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
