using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Player Thirst")]

    [SerializeField] private float maxThirst = 100f;
    [SerializeField] private float thirstOT = 0.02f;
    [SerializeField] private float currentThirst;
    [SerializeField] private ThirstBar thirstBar;
    [SerializeField] private float thirstDamage = 1f;

    [Header("Player Health")]

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private HealthBar healthBar;


    [Header("Player Hunger")]

    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float currentHunger;
    [SerializeField] private float HungerOT = 0.02f;
    [SerializeField] private HungerBar hungerBar;
    [SerializeField] private float hungerDamage = 0.05f;

    [Header("Player Stamina")]

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private StaminaBar staminaBar;
    [SerializeField] private float staminaRechargeRate = 2f;
    [SerializeField] private float staminaRechargeDelay = 1f;
    [SerializeField] private float staminaDecreaseRate = 1f;
    public bool HasStamina;
    private float currentStaminaDelayCounter;

    [Header("Player Sleep")]
    [SerializeField] private float maxSleep = 100f;
    [SerializeField] private float currentSleep;
    [SerializeField] private float SleepOT = 0.02f;
    [SerializeField] private SleepBar sleepBar;
    public Transform bed;
    public float sleepDistance = 2f;


    [Header("Player References")]
    [SerializeField] private StarterAssetsInputs playerInput;




    void Start()
    {
        currentThirst = maxThirst;
        thirstBar.SetMaxThirst(maxThirst);

        currentHunger = maxHunger;
        hungerBar.SetMaxHunger(maxHunger);

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);


        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        HasStamina = true;

        currentSleep = maxSleep;
        sleepBar.SetMaxSleep(maxSleep);
    }


    void Update()
    {
        currentThirst = currentThirst - thirstOT * Time.deltaTime;
        thirstBar.SetThirst(currentThirst);

        currentHunger = currentHunger - HungerOT * Time.deltaTime;
        hungerBar.SetHunger(currentHunger);

        currentSleep = currentSleep - SleepOT * Time.deltaTime;
        sleepBar.SetSleep(currentSleep);

        if (currentThirst <= 0)
        {
            currentHealth = currentHealth - thirstDamage * Time.deltaTime;
            currentThirst = 0;
            healthBar.SetHealth(currentHealth);
        }
        if (currentHunger <= 0)
        {
            currentHealth = currentHealth - hungerDamage * Time.deltaTime;
            currentHunger = 0;
            healthBar.SetHealth(currentHealth);
        }

        if (currentSleep <= 0)
        {
            staminaDecreaseRate = 3f;
            staminaRechargeRate = 0.5f;
            currentSleep = 0;
        }
        if (Input.GetKeyDown(KeyCode.J) && IsNearBed())
        {
            Sleep();
        }



        if (playerInput.sprint)
        {
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            staminaBar.SetStamina(currentStamina);
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                HasStamina = false;
            }
            currentStaminaDelayCounter = 0;
            if (currentStamina > 1)
            {
                HasStamina = true;
            }
        }

        if (!playerInput.sprint && currentStamina < maxStamina)
        {
            if (currentStaminaDelayCounter < staminaRechargeDelay)
                currentStaminaDelayCounter += Time.deltaTime;
            staminaBar.SetStamina(currentStamina);

        }
        if (currentStaminaDelayCounter >= staminaRechargeDelay)
        {
            currentStamina += staminaRechargeRate * Time.deltaTime;
            staminaBar.SetStamina(currentStamina);

            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }




        if (currentHealth <= 0)
        {
            Die();
        }



    }

    bool IsNearBed()
    {
        return Vector3.Distance(transform.position, bed.position) <= sleepDistance;
    }

    void Sleep()
    {
        currentSleep = 100;
        currentStamina = 100;
        staminaDecreaseRate = 1f;
        staminaRechargeRate = 2f;


    }


    void Die()
    {
        Debug.Log("Öldün");
    }
}
