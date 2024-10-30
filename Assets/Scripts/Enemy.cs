using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int MaxHealth;
    public int Health;

    public Image EnemyHealthBar;
    public Deck Deck;
    public List<Card> Hand = new List<Card>();
    private UIManager UIManager;

    public void DrawCard()
    {
        if (UIManager == null)
        {
            Debug.LogError("UIManager is null in DrawCard");
            return;
        }
        
        Card drawnCard = Deck.DrawCard();
        if (drawnCard == null)
        {
            Debug.LogError("Deck is null in enemy DrawCard");
            return;
        }
        Hand.Add(drawnCard);
        UIManager.LogCombatEvent("Enemy drew a card: " + drawnCard.Name);
    }

    public void DrawInitialHand()
    {
        int initialHandSize = 3; 
        for (int i = 0; i < initialHandSize; i++)
        {
            DrawCard();
        }
    }

    public void PlayCard(Card card)
    {
        card.PlayCard(CombatManager.Instance.Player, this); 
        Hand.Remove(card); 
        UIManager.LogCombatEvent("Enemy played a card! " + card.Name);
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        EnemyHealthBar.fillAmount = (float)Health / MaxHealth;
        if (Health <= MaxHealth * 0.2f)
        {
            Color color = EnemyHealthBar.color;
            color.a = 0.5f; 
            EnemyHealthBar.color = color;
            UIManager.LogCombatEvent("Enemy took damage: " + Damage + ", Health: " + Health);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        EnemyHealthBar.fillAmount = 1f;

        UIManager = FindFirstObjectByType<UIManager>();
        if (UIManager == null)
        {
            Debug.LogError("UIManager is null in Enemy Start");
        }

        Deck = FindFirstObjectByType<Deck>();
        if (Deck == null)
        {
            Deck = Resources.Load<Deck>("BasicDeck"); 
            if (Deck == null)
            {
                Debug.LogError("BasicDeck is not found in Resources");
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
