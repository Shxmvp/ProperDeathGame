using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Deck : ScriptableObject
{
    public List<Card> Cards = new List<Card>();

    public Card DrawCard()
    {
        if (Cards.Count > 0)
        {
            Card topCard = Cards[0];
            Cards.RemoveAt(0);
            return topCard;
        }
        else
        {   Debug.LogError("Deck is empty");
            return null; 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class DeckCreator
{
    [MenuItem("Assets/Create/Deck")]
    public static void CreateDeck()
    {
        Deck deck = ScriptableObject.CreateInstance<Deck>();
        AssetDatabase.CreateAsset(deck, "Assets/NewDeck.asset");
        AssetDatabase.SaveAssets();
    }
}
