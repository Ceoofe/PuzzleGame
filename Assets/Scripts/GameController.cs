using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool isEasyMode;

    GameObject cardOne;
    TMP_Text cardText;
    Image cardColor;
    Button cardBut;

    GameObject cardTwo;
    TMP_Text cardText2;
    Image cardColor2;
    Button cardBut2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEasyMode)
        {
            // 16 Cards
            // No timer
            // 5 Lives
        }
        // hard mode
        // 16 cards
        // Timer
        // 3 Lives
    }

    public void Card(GameObject obj)
    {
        if (!cardOne && !cardTwo)
        {
            cardOne = obj;
            cardText = cardOne.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            cardColor = cardOne.GetComponent<Image>();
            cardBut = cardOne.GetComponent<Button>();
            cardColor.color = Color.green;
            cardBut.interactable = false;
            Debug.Log(cardText.text);
        }
        else if (cardOne && !cardTwo)
        {
            cardTwo = obj;
            cardText2 = cardTwo.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            cardColor2 = cardTwo.GetComponent<Image>();
            cardBut2 = cardTwo.GetComponent<Button>();
            cardColor2.color = Color.green;
            cardBut2.interactable = false;
            Debug.Log(cardText2.text);
        }

        if (cardOne && cardTwo)
        {
            if (cardColor.color == Color.green && cardColor2.color == Color.green)
            {
                if (cardText.text == "Name1" && cardText2.text == "Name1")
                {
                    Debug.Log("1 point");
                    Destroy(cardOne);
                    Destroy(cardTwo);
                }
            }
        }


    }
}
