using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float maxMana = 100;
    private float currentHealth;
    private float currentMana;
    //private float addedHealth;
    [SerializeField] private int ManaRegenerationAmount = 5;
    [SerializeField] private int HealthRegenerationAmount = 0;
    [SerializeField] private Bar healthBar;
    [SerializeField] private Bar manaBar;



    void Start()
    {
        currentHealth = maxHealth;
        currentMana = 10;
        healthBar.UpdateHealthBar(maxHealth, currentHealth, HealthRegenerationAmount);
        manaBar.UpdateManaBar(maxMana, currentMana, ManaRegenerationAmount);

        StartCoroutine(RegenerateRoutine());
    }

    void Update()
    {

    }

    private IEnumerator RegenerateRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(1);
            PlayerUpdateMana(ManaRegenerationAmount);
            PlayerUpdateHealth(HealthRegenerationAmount);
        }
    }

    public void PlayerUpdateHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth <= 0)
        {
            healthBar.UpdateHealthBar(maxHealth, 0, 0);
            PlayerDie();
        }
        else
        {
            currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
            currentHealth = currentHealth < 0 ? 0 : currentHealth;
            healthBar.UpdateHealthBar(maxHealth, currentHealth, HealthRegenerationAmount);
        }
    }
    public void PlayerUpdateMana(float manaAmount)
    {
        currentMana += manaAmount;
        currentMana = currentMana > maxMana ? maxMana : currentMana;
        currentMana = currentMana < 0 ? 0 : currentMana;
        manaBar.UpdateManaBar(maxMana, currentMana, ManaRegenerationAmount);
    }

    public int GetCurrentMana() {
        return (int)currentMana;
    }

    public void PlayerUpdateRegenerationSpeed(float amount) {
        HealthRegenerationAmount = (int)amount;
    }
    
    private void PlayerDie()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("got hit by " + collision.gameObject.name);

        switch (collision.gameObject.name)
        {
            case "ColiderSword":
                PlayerUpdateHealth(SkeletonController.Damage);
                break;
            case "EnemyFireBall(Clone)":
                PlayerUpdateHealth(EnemyCasterController.Damage);
                break;
            case "LeftHandSphere":
                PlayerUpdateHealth(EnemyMeleeController.Damage);
                break;
            case "RightHandSphere":
                PlayerUpdateHealth(EnemyMeleeController.Damage);
                break;
            default:
                break;
        }
    }
}
