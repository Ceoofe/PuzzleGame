using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool isEasyMode;
    public static bool isHardMode;
    public static bool gameOver;
    public static int score = 0;

    GameObject cardOne;
    TMP_Text cardText;
    Image cardColor;
    Button cardBut;

    GameObject cardTwo;
    TMP_Text cardText2;
    Image cardColor2;
    Button cardBut2;

    GameObject border;
    GridLayoutGroup grid;
    List<GameObject> cardList = new();
    Transform health;
    readonly GameObject[] hearts = new GameObject[5];

    AudioSource audioSorce;
    public AudioClip[] clips;
    TMP_Text scoreText;
    TMP_Text timerText;
    float gameTimer = 60f;
    float timer;
    readonly float waitTime = .5f;
    public int heart = 5;
    readonly string[] cardTypes = { "Bed", "Chair", "Fruit Bowl", "Table", "Vase", "TV", "Lamp", "NoteBook" };
    Color originalColor;
    bool starting;
    bool isReady;
    bool isWrong;
    bool startTime;
    bool threeLives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSorce = GetComponent<AudioSource>();
        border = transform.Find("Border").gameObject;
        grid = border.GetComponent<GridLayoutGroup>();
        health = transform.Find("Health");
        scoreText = transform.Find("Score/ScoreText").GetComponent<TMP_Text>();
        timerText = transform.Find("Timer").GetComponent<TMP_Text>();
        score = 0;
        scoreText.text = "Score: " + score; // Updates score UI

        foreach (Button btn in border.GetComponentsInChildren<Button>()) // Buttons not interactable when game starts
        {
            btn.interactable = false;
        }
        for (int i = 1; i < health.childCount; i++) // Gets the hearts of the player
        {
            hearts[i - 1] = health.GetChild(i).gameObject;
        }
        for (int i = 0; i < border.transform.childCount; i++) // Gets all the cards
        {
            cardList.Add(border.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEasyMode && !isHardMode) // Easy Mode
        {
            // No timer
            // 5 Lives
            timerText.transform.gameObject.SetActive(false);
            if (starting == false)
            {
                StartCoroutine(Randomize());// randomize cards and set images active to false
                starting = true;
            }
            isEasyMode = false;
            isHardMode = false;
        }
        else if (isHardMode && !isEasyMode) // Hard Mode
        {
            // Timer
            // 3 Lives
            timerText.transform.gameObject.SetActive(true);
            if (starting == false)
            {
                StartCoroutine(Randomize());// randomize cards and set images active to false
                starting = true;
            }
            if (!threeLives) // Three Lives
            {
                hearts[4].SetActive(false);
                hearts[3].SetActive(false);
                heart -= 2;
                threeLives = true;
            }
            isEasyMode = false;
            isHardMode = false;
        }
        if (startTime)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = ((int)gameTimer).ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (border.transform.childCount <= 0) // When the player wins show the score and the "You Win!" and go back button
        {
            SceneManager.LoadScene("Result");
            gameOver = false;
        }
        if (!hearts[0].activeSelf || gameTimer <= 0) // When the player loses
        {
            SceneManager.LoadScene("Result");
            gameOver = true;
        }

        if (isReady) // Wait for right
        {
            timer += Time.deltaTime;
            foreach (Button btn in border.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            if (timer > waitTime)
            {
                foreach (Button btn in border.GetComponentsInChildren<Button>())
                {
                    btn.interactable = true;
                }
                isReady = false;
                cardList.Remove(cardOne);
                cardList.Remove(cardTwo);
                Destroy(cardOne);
                Destroy(cardTwo);
                timer = 0;
            }    
        }
        if (isWrong) // Wait for wrong
        {
            timer += Time.deltaTime;
            foreach (Button btn in border.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            // red cards
            cardColor.color = Color.red;
            cardColor2.color = Color.red;
            if (timer > waitTime)
            {
                foreach (Button btn in border.GetComponentsInChildren<Button>())
                {
                    btn.interactable = true;
                }
                // Revert the cards back to where they were
                cardText.enabled = false;
                cardOne.transform.Find("Image").GetComponent<Image>().enabled = false;
                cardText2.enabled = false;
                cardTwo.transform.Find("Image").GetComponent<Image>().enabled = false;
                cardColor.color = originalColor;
                cardColor2.color = originalColor;
                cardOne = null;
                cardTwo = null;
                isWrong = false;
                timer = 0;
            }
        }
    }

    public void Card(GameObject obj)
    {
        if (!cardOne && !cardTwo) // Picks card one
        {
            cardOne = obj;
            cardText = cardOne.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            cardColor = cardOne.GetComponent<Image>();
            cardBut = cardOne.GetComponent<Button>();
            originalColor = cardColor.color;
            cardColor.color = Color.green;
            cardText.enabled = true;
            cardOne.transform.Find("Image").GetComponent<Image>().enabled = true;
            cardBut.interactable = false;
            Debug.Log(cardText.text);
        }
        else if (cardOne && !cardTwo) // picks card two
        {
            cardTwo = obj;
            cardText2 = cardTwo.transform.Find("Text (TMP)").GetComponent<TMP_Text>();
            cardColor2 = cardTwo.GetComponent<Image>();
            cardBut2 = cardTwo.GetComponent<Button>();
            cardColor2.color = Color.green;
            cardText2.enabled = true;
            cardTwo.transform.Find("Image").GetComponent<Image>().enabled = true;
            cardBut2.interactable = false;
            Debug.Log(cardText2.text);
        }

        if (cardOne && cardTwo && cardColor.color == Color.green && cardColor2.color == Color.green) // If both cards are selected
        {
            for (int i = 0; i < cardTypes.Length; i++) // length
            {
                if (cardText.text == cardTypes[i] && cardText2.text == cardTypes[i] && cardOne.name != cardTwo.name) // Both cards match
                {
                    Debug.Log("1 point");
                    score++;
                    scoreText.text = "Score: " + score; // Updates score UI
                    audioSorce.clip = clips[0];
                    audioSorce.Play();
                    isReady = true;
                }
                if (cardText.text == cardTypes[i] && cardText2.text != cardTypes[i] && cardOne.name != cardTwo.name || cardText.text != cardTypes[i] && cardText2.text == cardTypes[i] && cardOne.name != cardTwo.name)
                {
                    if (score > 0)
                    {
                        Debug.Log("-1 point");
                        score--;
                        scoreText.text = "Score: " + score; // Updates score UI
                    }
                    if (heart > 0)
                    {
                        hearts[heart - 1].SetActive(false);// Remove heart
                        heart--;
                    }
                    audioSorce.clip = clips[1];
                    audioSorce.Play();
                    isWrong = true;
                    break;
                }
            }
        }
    }
    IEnumerator Randomize()
    {
        // randomize the cards
        for (int i = 0; i < cardList.Count; i++)
        {
            int randomIndex = Random.Range(i, cardList.Count);
            (cardList[randomIndex], cardList[i]) = (cardList[i], cardList[randomIndex]);
        }
        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].transform.SetSiblingIndex(i);
        }
        yield return new WaitForSeconds(.1f);
        grid.enabled = false;
        yield return new WaitForSeconds(3f);
        foreach (Image image in border.GetComponentsInChildren<Image>())
        {
            if (image.name == "Image")
            {
                image.enabled = false;
            }
        }
        foreach (TMP_Text text in border.GetComponentsInChildren<TMP_Text>())
        {
            if (text.name == "Text (TMP)")
            {
                text.enabled = false;
            }
        }
        foreach (Button btn in border.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
        }
        // Start timer here
        if (!hearts[4].activeSelf && !hearts[3].activeSelf)
        {
            startTime = true;
        }
    }
}
