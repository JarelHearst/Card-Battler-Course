using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Slider healthSlider;

    [SerializeField] private int totalHealth = 100;
    
    private int currentHealth;

    private Flash flash;

    private void Awake()
    {
        flash = GetComponentInChildren<Flash>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
       currentHealth = totalHealth; 
       UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthText.text = currentHealth + "/" + totalHealth;
        healthSlider.maxValue = totalHealth;
        healthSlider.value = currentHealth;
    }

    public void HealDamage(int amount)
    {

        if(amount <= 0)
        {
            return;
        }

        currentHealth += amount;

        if(currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        StartCoroutine(flash.FlashRoutine());
        currentHealth -= amount;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
