using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class cubeButton : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Cube was clicked!");
        //CardManager cardManager = new CardManager();
        //cardManager.DrawCard();
        
        DrawCard();
    }
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public void DrawCard()
    {
        Debug.Log(deck.Count);


        Card randCard = deck[Random.Range(0, deck.Count)];

        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                randCard.gameObject.SetActive(true);
                randCard.transform.position = cardSlots[i].position;
                availableCardSlots[i] = false;
                deck.Remove(randCard);
                return;
            }
        }
    }
}
