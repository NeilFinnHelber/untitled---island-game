using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
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
