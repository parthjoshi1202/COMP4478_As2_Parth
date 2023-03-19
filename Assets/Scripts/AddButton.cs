//Setting up Buttons for the game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    //Rect Transform Game Object to display the cards
    [SerializeField]
    private Transform cardPanel;

    [SerializeField]
    private GameObject btn;

    //Displaying 16 instances/copy of the card using 1 prefab as the screen should be 4x4
    //Also numbering the cards and setting it as the parent
    void Awake()
    {
        for (int i=0; i<16; i++)
        {
            GameObject card = Instantiate(btn);
            card.name = "" + i;
            card.transform.SetParent(cardPanel, false);
        }
    }
}
