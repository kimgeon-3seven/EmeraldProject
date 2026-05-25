using UnityEngine;
using System.Collections.Generic;

public class HpController : MonoBehaviour
{
    [Header("HP Settings")]
    [SerializeField] private float maxHp = 100f;
    private float currentHp;

    [Header("State")]
    private bool isDead = false;
    public bool IsDead => isDead;


    private Renderer targetRenderer;
    private Color originalColor;
   

    private void Awake()
    {
        currentHp = maxHp;

        
        targetRenderer = GetComponentInChildren<Renderer>();
        if (targetRenderer != null)
        {
            originalColor = targetRenderer.material.color;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHp = Mathf.Clamp(currentHp - damage, 0f, maxHp);
        Debug.Log($"[피격] {gameObject.name}의 현재 HP: {currentHp}/{maxHp}");

        
        if (targetRenderer != null)
        {
            
            targetRenderer.material.color = Color.red;
            
            Invoke(nameof(ResetColor), 0.1f);
        }
        // ----------------------------------

        if (currentHp <= 0f)
        {
            Die();
        }
    }

    
    private void ResetColor()
    {
        if (targetRenderer != null && !isDead)
        {
            targetRenderer.material.color = originalColor;
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;
        currentHp = Mathf.Clamp(currentHp + amount, 0f, maxHp);
        Debug.Log($"[회복] {gameObject.name}의 현재 HP: {currentHp}/{maxHp}");

       
        if (targetRenderer != null)
        {
            targetRenderer.material.color = Color.green;
            Invoke(nameof(ResetColor), 0.1f);
        }
   
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"[사망] {gameObject.name}");
        gameObject.SetActive(false);
    }
}