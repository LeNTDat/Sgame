using TMPro;
using UnityEngine;

public class DisplayPName : MonoBehaviour
{
    [SerializeField] TMP_Text PlayerName;
    [SerializeField] bool isShow = false;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!isShow && gameManager.IsStart)
        {
            DisplayName();
        }
    }

    public void DisplayName ()
    {
        PlayerName.text = gameObject.name;
        isShow = true;
    }
}
