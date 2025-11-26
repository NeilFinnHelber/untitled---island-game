using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class cubeButton : MonoBehaviour
{
    //to load a gameObject into the class and use it in the code
    [SerializeField]
    private GameObject CardManager;
    void OnMouseDown()
    {
        Debug.Log("Cube was clicked!");
        
        //call a method from this card
        CardManager.GetComponent<CardManager>().DrawCard();
        
    }
}
