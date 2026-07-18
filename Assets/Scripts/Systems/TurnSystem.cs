using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    [SerializeField] private int maxActionsPerTurn = 1;

    [SerializeField] private int drawCost = 1;

    [SerializeField] private int reshuffleCost = 3;

    [SerializeField] private int turnWaitTime = 3;

    [SerializeField] private TextMeshProUGUI remainingActionsText;

    [SerializeField] private TextMeshProUGUI displayTurnState;

    [SerializeField] private float bossDelayTime = 2f;
    
    private int actionsRemaining;

    //private bool bossDead = false;

    private enum TurnState {PlayerTurn, BossTurn}

    private TurnState currentTurn = TurnState.PlayerTurn;

    private void Start()
    {
        displayTurnState.text = "Player's Turn";
        StartPlayerTurn();
    }

    private void OnEnable()
    {
        PlayerEvents.OnDrawCardRequested += DrawRequested;
        PlayerEvents.OnCardPlayed += CardPlayed;
        PlayerEvents.OnReshuffleRequested += ReshuffleRequested;
        PlayerEvents.OnPlayerDeath += ClearTurnDisplay;
        BossEvents.OnBossDeath += ClearTurnDisplay;
        
    }

    private void OnDisable()
    {
        PlayerEvents.OnDrawCardRequested -= DrawRequested;        
        PlayerEvents.OnCardPlayed -= CardPlayed;
        PlayerEvents.OnReshuffleRequested -= ReshuffleRequested;
        BossEvents.OnBossDeath -= ClearTurnDisplay;
        PlayerEvents.OnPlayerDeath -= ClearTurnDisplay;

        
    }

    private void StartPlayerTurn()
    {
        Debug.Log("StartPlayerTurn");
        currentTurn = TurnState.PlayerTurn;
        actionsRemaining = maxActionsPerTurn;
        UpdateActionsUI();
        TurnEvents.PlayerTurnStart();
    }

    private void EndPlayerTurn()
    {
        Debug.Log("EndPlayerTurn");
        TurnEvents.PlayerTurnEnd();
        StartCoroutine(WaitBetweenTurns());
    }

    private IEnumerator StartBossTurn()
    {
        Debug.Log("StartBossTurn");
        currentTurn = TurnState.BossTurn;
        yield return new WaitForSeconds(bossDelayTime);
        BossTurn();
    }

    private IEnumerator EndBossTurn()
    {
        Debug.Log("EndBossTurn");
        TurnEvents.BossTurnEnd();
        yield return new WaitForSeconds(bossDelayTime);
        StartCoroutine(WaitBetweenTurns());
    }

    private void ClearTurnDisplay()
    {
        displayTurnState.text = "";
    }

    private IEnumerator WaitBetweenTurns()
    {
        for (int i = turnWaitTime; i > 0; i--)
        {
            displayTurnState.text = i + "...";
            yield return new WaitForSeconds(1f);
        }
        
        if(GameManager.Instance.IsGameActive())
        {
           if(currentTurn != TurnState.PlayerTurn)
            {
                StartPlayerTurn();
                displayTurnState.text = "Player's Turn";
            }
            else
            {
                displayTurnState.text = "Boss's Turn";
                StartCoroutine(StartBossTurn());
            } 
        }
    }

    private void CardPlayed(CardData cardData)
    {
        ConsumeAction(cardData.actionCost);
    }

    private void DrawRequested()
    {
        ConsumeAction(drawCost);
    }

    private void ReshuffleRequested()
    {
        ConsumeAction(reshuffleCost);
    }

    public bool HasActionsRemaining()
    {
        return actionsRemaining > 0;
    }
    
    private void ConsumeAction(int amount)
    {
        actionsRemaining -= amount;
        UpdateActionsUI();
        if(actionsRemaining <= 0)
        {
            EndPlayerTurn();           
        }
    }

    private void BossTurn()
    {
        TurnEvents.BossTurnStart();
        StartCoroutine(EndBossTurn());
    }

    private void UpdateActionsUI()
    {
        if(actionsRemaining < 0)
        {
            actionsRemaining = 0;
        }

        remainingActionsText.text = "Remaining Actions: " + actionsRemaining;
    }
}
