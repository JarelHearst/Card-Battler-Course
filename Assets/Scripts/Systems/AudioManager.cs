using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioClip playCardSFX;

    [SerializeField] private AudioClip drawCardSFX;

    [SerializeField] private AudioClip reshuffleCardSFX;

    [SerializeField] private AudioClip playerHitSFX;

    [SerializeField] private AudioClip playerDeathSFX;

    [SerializeField] private AudioClip playerHealSFX;

    [SerializeField] private AudioClip bossDeathSFX;

    [SerializeField] private AudioClip bossHitSFX;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerEvents.OnCardPlayed += CardPlayed;
        PlayerEvents.OnDrawCardRequested += DrawCardRequested;
        PlayerEvents.OnReshuffleRequested += ReshuffleRequested;
        PlayerEvents.OnPlayerHit += PlayerHit;
        PlayerEvents.OnPlayerDeath += PlayerDeath;
        PlayerEvents.OnPlayerHeal += PlayerHeal;

        BossEvents.OnBossHit += BossHit;
        BossEvents.OnBossDeath += BossDeath;
    }

    private void OnDisable()
    {
        PlayerEvents.OnCardPlayed -= CardPlayed;
        PlayerEvents.OnDrawCardRequested -= DrawCardRequested;
        PlayerEvents.OnReshuffleRequested -= ReshuffleRequested;        
        PlayerEvents.OnPlayerHit -= PlayerHit;
        PlayerEvents.OnPlayerDeath -= PlayerDeath; 
        PlayerEvents.OnPlayerHeal -= PlayerHeal; 
                      
        BossEvents.OnBossHit -= BossHit;
        BossEvents.OnBossDeath -= BossDeath;
    }

    private void CardPlayed(CardData _)
    {
        PlaySFX(playCardSFX);
    }

    private void DrawCardRequested()
    {
        PlaySFX(drawCardSFX);
    }

    private void ReshuffleRequested()
    {
        PlaySFX(reshuffleCardSFX);
    }

    private void PlayerHit(int _)
    {
        PlaySFX(playerHitSFX);
    }
    
    private void PlayerDeath()
    {
        PlaySFX(playerDeathSFX);
    }
    
    private void PlayerHeal()
    {
        PlaySFX(playerHealSFX);
    }

    private void BossDeath()
    {
        PlaySFX(bossDeathSFX);
    }
    
    private void BossHit(CardData _)
    {
        PlaySFX(bossHitSFX);
    }
    
    private void PlaySFX(AudioClip audioClip)
    {
        if (audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
