/*
This controls the logic of the game
In essence, it sets the sprites at different positions using the Random function
It ensures only 2 cards are picked, if they don't match, they're flipped again
If they match, they disappear, once all of them are matched, it geenrates next scene 
To prompt the user to play again, and allocating random positions to the sprites each time
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Storing the buttons in a List
    public List<Button> btns = new List<Button>();

    //Storing Sprites in the array
    public Sprite[] cards;

    public List<Sprite> gameCards = new List<Sprite>();

    //other side of the card ("blank.jpg")
    [SerializeField]
    private Sprite cardImage;


    //Variables to calculate and regulate amount of guesses, their indexes and the name
    //Of the sprites
    private bool guess1, guess2;

    private int countGuessCorrect, gameGuess, guess1index, guess2index, countGuess;

    private string guess1name, guess2name; 

    //Load all the sprites from a path 
    void Awake()
    {
        cards = Resources.LoadAll<Sprite>("Sprites/Card Contents");
    }

    //Calling all the functions 
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGameCards();
        gameGuess = gameCards.Count/2;
        RandomCard(gameCards);
    }

    //Adding buttons in the screen which have the CardButton tag
    //And setting the other side of the card to all buttons ("blank.jpg") for the 16 cards
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("CardButton");

        for(int i=0; i<objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = cardImage;
        }
    }

    //Ensuring that there are 16 sprites generated, from 8 given sprites
    //since the screen is 4x4, and ensuring there are 2 cards each to match them 
    void AddGameCards()
    {
        int looper = btns.Count;
        int index = 0;

        for(int i =0; i<looper; i++)
        {
            if(index== looper/2)
            {
                index = 0;
            }
            gameCards.Add(cards[index]);

            index++;
        }
    }
    
    //Adding event listener and referencing the next function
    //Looping through each 16 buttons 
    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickCard());
        }
    }

    /*
    This functions ensures only 2 cards are picked, 
    if they don't match, they're flipped again
    If they match, they disappear, once all of them are matched, it geenrates next scene 
    To prompt the user to play again, and allocating random positions to the 
    sprites each time 
    */

    //If the user didn't guess the card for first time, the guess is set to true 
    //Index of the sprite is obtained to store in the first guess
    public void PickCard()
    {
        if(!guess1)
        {
            guess1 = true;
            guess1index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            guess1name = gameCards[guess1index].name;
            btns[guess1index].image.sprite = gameCards[guess1index];
        }

        //If the user didn't guess the card for second time, the guess is set to true 
        //Index of the sprite is obtained to store in the second guess

        else if (!guess2) 
        { 
            guess2 = true;
            guess2index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            guess2name = gameCards[guess2index].name;
            btns[guess2index].image.sprite = gameCards[guess2index];
            countGuess++;

            //checking if both guesses match or not 
            StartCoroutine(checkMatch());
        }
    }

    //checking if both guesses match or not 
    //it waits for a second, if both the guesses match (if both the sprites match)
    //the buttons become unclickable, the card disappears, using the index of that card
    IEnumerator checkMatch()
    {
        yield return new WaitForSeconds(1f);

        if(guess1name == guess2name)
        {
            yield return new WaitForSeconds(0.5f);
            btns[guess1index].interactable = false; 
            btns[guess2index].interactable = false;

            btns[guess1index].image.color = new Color(0, 0, 0, 0);
            btns[guess2index].image.color = new Color(0, 0, 0, 0);

            checkFinish();
        }

        //otherwise they flip back and user can select another card
        else
        {
            btns[guess1index].image.sprite = cardImage;
            btns[guess2index].image.sprite = cardImage;
        }

        yield return new WaitForSeconds(0.5f);
        guess1 = guess2 = false;
    }

    //This function counts the guesses, if they are equal to the amounts of cards
    //and when all the cards match with their sprites, the Scene Manager promptes the user 
    //to play again, thus restarting the game with new positions of sprites hidden by
    //blank.jpg , the other side of the card
    void checkFinish()
    {
        countGuessCorrect++;
        if(countGuessCorrect == gameGuess) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
        }
    }


    //This function allots the placement of the 8 sprites randomly,
    //each time the user plays
    void RandomCard(List<Sprite> list)
    {
        //Iterating through the list with a given range of the list count
        for(int i=0; i< list.Count; i++)
        {
            Sprite sprite = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = sprite;
        }
    }
}
