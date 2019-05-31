using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; 


public class Health : NetworkBehaviour
{
    public const int maxHealth = 100; //players always start with 100 health
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth; //call OnChangeHealth if the value of current health changes
    public RectTransform healthbar; //Get the gui healthbar to move so the players see the health depleate 

    public void TakeDamage(int amount) //taking damage command
    {
        if(!isServer) //ensures only the server handles this, otherwise players would get hit twice
        {
            return;
        }
        currentHealth -= amount; //change health value
        if (currentHealth <= 0)
        {
            currentHealth = 0; //it would be bad if the health went - as it would affect the health display
            Debug.Log("Die");
        }

       
    }

    void OnChangeHealth(int currentHealth)
    {
        healthbar.sizeDelta = new Vector2(currentHealth * 2, healthbar.sizeDelta.y); //change size of health bar
    }

    public int getHealth()
    {
        return currentHealth;
    }
}
