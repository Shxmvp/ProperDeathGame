using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Numerics;

public class Player : MonoBehaviour
{
    public UnityEngine.Vector2 moveValue;
    public float moveSpeed = 5f;
    public LayerMask terrainLayer;
    private CharacterController characterController;
    public int MaxHealth = 100;
    public int Health;
    public Image PlayerHealthBar;
    public int MaxExp = 100;
    public int Exp;
    public Deck Deck;
    public UIManager UIManager;

    public List<Card> Hand = new List<Card>();


    void OnMove(InputValue value)
    { 
        moveValue = value.Get<UnityEngine.Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arena")
        {
            if (CombatManager.Instance == null)
            {
                Debug.LogError("CombatManager.Instance is null");
            }
            else
            {
                UIManager.ShowBattlePopup();
            }
        }
    }

    public void DrawCard()
    {
        Card drawnCard = Deck.DrawCard();
        if (drawnCard == null)
        {
            Debug.LogError("deck is null in player DrawCard");
            return;
        }
        Hand.Add(drawnCard);
        UIManager.LogCombatEvent("Player drew a card: " + drawnCard.Name);
    }

    public void DrawInitialHand()
    {
        int initialHandSize = 3; 
        for (int i = 0; i < initialHandSize; i++)
        {
            DrawCard();
        }
    }

    //Eventaully add ability to out card back

    public void PlayCard(Card card)
    {
        card.PlayCard(this, CombatManager.Instance.Enemy); 
        //Exp -= CombatMCost;
        Hand.Remove(card); 
        UIManager.LogCombatEvent("Player played a card: " + card.Name);

    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage; 
        PlayerHealthBar.fillAmount = (float)Health / MaxHealth;
        if (Health <= MaxHealth * 0.2f)
        {
            PlayerHealthBar.color = Color.red;
        }
        UIManager.LogCombatEvent("Player took damage: " + Damage + ", Health: " + Health);
    }

    public void Heal(int amount)
    {
        Health = Mathf.Min(Health + amount, MaxHealth);
        PlayerHealthBar.fillAmount = (float)Health / MaxHealth;
        UIManager.LogCombatEvent("Player healed: " + amount + ", Health: " + Health);
    }


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        UIManager = FindFirstObjectByType<UIManager>();
        Health = MaxHealth;
        PlayerHealthBar.fillAmount = 1f;
        Exp = MaxExp;
        
        if (UIManager == null)
        {
            Debug.LogError("UIManager is null in Player Start");
        }

        Deck = FindFirstObjectByType<Deck>();
        if (Deck == null)
        {
            Deck = Resources.Load<Deck>("BasicDeck"); // Load the default deck
            if (Deck == null)
            {
                Debug.LogError("BasicDeck is not found in Resources");
            }
        }

     
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the new position
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;

        // Apply the movement to the player's position
        characterController.Move(movement);

    }
}
