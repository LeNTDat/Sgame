using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class DisplayPName : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    TMP_InputField inputField;
    

    void Start()
    {
        print(inputField.text);
    }

    void Update()
    {
        
    }
}
