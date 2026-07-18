using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static event Action<CardData> OnCardPlayed;

    public static event Action<int> OnPlayerHit;

    public static event Action OnPlayerHeal;

    public static event Action OnPlayerDeath;

    public static event Action OnAttackComplete;

    public static event Action OnDrawCardRequested;

    public static event Action OnReshuffleRequested;

    public static void CardPlayed(CardData cardData)
    {
        OnCardPlayed?.Invoke(cardData);
    }

    public static void PlayerHit(int damage)
    {
        OnPlayerHit?.Invoke(damage);
    }

    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void AttackComplete()
    {
        OnAttackComplete?.Invoke();
    }
    
    public static void PlayerHeal()
    {
        OnPlayerHeal?.Invoke();
    }
    public static void DrawCardRequested()
    {
        OnDrawCardRequested?.Invoke();
    }

    public static void ReshuffleRequested()
    {
        OnReshuffleRequested?.Invoke();
    }
}
