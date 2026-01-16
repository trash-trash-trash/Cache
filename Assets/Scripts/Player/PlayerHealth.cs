using UnityEngine;

public class PlayerHealth : Health
{
    public int normalMaxHealth = 3;
    public int hardMaxHealth = 1;

    public int deathCounter = 0;
    
    public override void Rez()
    {
        if (AAAGameManager.Instance != null)
        {
            if (AAAGameManager.Instance.currentDifficulty == Difficulty.Normal)
                maxHealth = normalMaxHealth;
            else
                maxHealth = hardMaxHealth;
        }

        base.ChangeHealth(maxHealth);
        AnnounceIsAlive += AddToDeathCounter;
    }

    //reset on load level
    private void AddToDeathCounter(bool isAlive)
    {
        if(!isAlive)
            deathCounter++;
    }

    void OnDisable()
    {
        AnnounceIsAlive -= AddToDeathCounter; 
    }
}
