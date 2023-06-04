using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] List<GameObject> listOfFinish;
    [SerializeField] GameObject[] placeFields;
    [SerializeField] GameObject scoreBoard;
    [SerializeField] bool isDisplayBoard = true;
    GameManager gameManager;
    void Start()
    {
        listOfFinish = new List<GameObject>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.IsEndGame && isDisplayBoard)
        {
            scoreBoard.SetActive(true);
            DisplayScoreBoard();
        }
    }
  
    void DisplayScoreBoard () {
        for (int i = 0; i < listOfFinish.Count; i++)
        {
            foreach (Transform child in placeFields[i].transform)
            {
                switch (child.name)
                {
                    case "Place":
                        break;
                    case "PlayerName":
                        child.GetComponent<TMP_Text>().text = listOfFinish[i].name; 
                        break;
                    case "Turn":
                        child.GetComponent<TMP_Text>().text = listOfFinish[i].GetComponent<PlayerMovement>().NumberOfTurn.ToString();
                        break;
                    case "Bonus":
                        child.GetComponent<TMP_Text>().text = listOfFinish[i].GetComponent<PlayerMovement>().NumberOfBonus.ToString();
                        break;
                    case "Trap":
                        child.GetComponent<TMP_Text>().text = listOfFinish[i].GetComponent<PlayerMovement>().NumberOfTrap.ToString();
                        break;


                }
            }
        }
        isDisplayBoard = false;
    }
    
    public void GetFinishList(GameObject item, Vector3 pos)
    {
        GameObject player = Instantiate(item, pos, Quaternion.identity);
        player.name = item.name;
        listOfFinish.Add(player);
    }
}