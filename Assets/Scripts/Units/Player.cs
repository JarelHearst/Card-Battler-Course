using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerSprite;

    private Vector3 originalPosition;
    
    private Animator animationController;

    private Health health;

    private ParticleSystem healVFX;

    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += HandleCardPlayed;
        PlayerEvents.OnPlayerHit += HandlePlayerHit;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= HandleCardPlayed;
        PlayerEvents.OnPlayerHit -= HandlePlayerHit;
    }

    private void Awake()
    {
        animationController = playerSprite.GetComponent<Animator>();
        health = GetComponent<Health>();
        healVFX = playerSprite.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        originalPosition = playerSprite.transform.position;
    }
    
    private void HandleCardPlayed(CardData cardData)
    {
        if(cardData.attackPower > 0 )
        {   
            Attack(cardData);       
        }

        if(cardData.healPower > 0)
        {
            Heal(cardData);
        }
    }
    
    private void HandlePlayerHit(int damage)
    {
        animationController.Play("Hurt");
        health.TakeDamage(damage);
        if (!health.IsAlive())
        {
            //Die
            Die();
        }
    }

    private void Attack(CardData cardData)
    {
        StartCoroutine(PlayerAttackAnimation(cardData));
    }

    private void Heal(CardData cardData)
    {
        health.HealDamage(cardData.healPower);
        healVFX.Play();
        PlayerEvents.PlayerHeal();
    }

    private void Die()
    {
        animationController.Play("Death");
        PlayerEvents.PlayerDeath();
    }

    private IEnumerator PlayerAttackAnimation(CardData cardData)
    {
        Vector3 targetPosition = originalPosition + new Vector3(4f, 0, 0);
        float duration = .5f;
        float timeElapsed = 0f;

        while(timeElapsed < duration)
        {
            playerSprite.transform.position = Vector3.Lerp(originalPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (cardData.isDoubleAttack)
        {
            animationController.Play("DoubleAttack");
        }

        else
        {   
            animationController.Play("Attack");
        }
        BossEvents.BossHit(cardData);
        yield return new WaitForSeconds(.5f);
        timeElapsed = 0f;

        while(timeElapsed < duration)
        {
            playerSprite.transform.position = Vector3.Lerp(targetPosition, originalPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        PlayerEvents.AttackComplete();
        yield return null;
    }
}
