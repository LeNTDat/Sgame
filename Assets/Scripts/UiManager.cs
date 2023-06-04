using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject systemPanel;
    [SerializeField] GameObject PlayerChoice;
    [SerializeField] GameObject playerNamePanel;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        DisplayPanel();
        DisplaySystemPanel();
    }

    void DisplaySystemPanel()
    {
        foreach (Transform child in systemPanel.transform) { 
            if(!gameManager.IsEndTurn) {
                if(child.gameObject.name == "Rolled")
                {
                    child.GetComponent<Image>().enabled = true;
                }
            }
            if (gameManager.IsEndTurn)
            {
                if (child.gameObject.name == "Rolled")
                {
                    child.GetComponent<Image>().enabled = false;
                }
            }
        }
    }

    void DisplayPanel()
    {
        if (gameManager.IsSumit)
        {
            playerNamePanel.SetActive(true);
            PlayerChoice.SetActive(false);
        }
        if (gameManager.isStart)
        {
            playerNamePanel.SetActive(false);
            systemPanel.SetActive(true);   
        }
        if (gameManager.IsEndGame)
        {
            systemPanel.SetActive(false);
        }
    }
}
