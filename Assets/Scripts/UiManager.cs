using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject systemPanel;
    [SerializeField] GameObject PlayerChoice;
    [SerializeField] GameObject playerNamePanel;
    [SerializeField] GameObject playerTurnNamePanel;
    [SerializeField] TMP_Text displayPlayerName;
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

    public void ShowTurnName(string name)
    {
        StartCoroutine(TurnName(name));
    }
    IEnumerator TurnName(string name)
    {
        displayPlayerName.text = name + " is running ...";
        playerTurnNamePanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerTurnNamePanel.SetActive(false);
        yield return null;
    }

    void DisplaySystemPanel()
    {
        foreach (Transform child in systemPanel.transform) { 
            if (!gameManager.IsEndTurn) {
                switch (child.gameObject.name)
                {
                    case "Rolled":
                        child.GetComponent<Image>().enabled = true;
                        break;
                    case "Ended":
                        child.GetComponent<Image>().enabled = false;
                        break;
                }
            }
            if (gameManager.IsEndTurn)
            {
                switch (child.gameObject.name)
                {
                    case "Rolled":
                        child.GetComponent<Image>().enabled = false;
                        break;
                    case "Ended":
                        child.GetComponent<Image>().enabled = true ;
                        break;
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
        if (gameManager.IsStart)
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
