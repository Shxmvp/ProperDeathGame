using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Description;
    public int Cost; // Experience cost to play the card (int)
    public int Attack;    
    private UIManager UIManager;
    public void PlayCard(Player player, Enemy enemy)
    {
        // Check if the player has enough experience to play the card
        if (player.Exp >= Cost)
        {
            // Deduct the cost from the player's experience
            player.Exp -= Cost;

            // Apply the card's effect
            if (Attack > 0)
            {
                // Attack card: deal damage to the enemy
                enemy.TakeDamage(Attack);
            }
            else if (Attack < 0)
            {
                // Heal card: heal the player
                player.Heal(-Attack);
            }
        }
        else
        {
            UIManager.LogCombatEvent("Not enough experience to play this card.");
        }
    }

}




