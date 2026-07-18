using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    [SerializeField] private List<CardData> availableCards;

    [SerializeField] private Transform[] cardSlots;
    
    [SerializeField] private GameObject cardPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < availableCards.Count; i++)
        {
            AddCardToCollection(i);
        }
    }

    private void AddCardToCollection(int cardIndex)
    {
        GameObject card = Instantiate(cardPrefab, cardSlots[cardIndex].position, quaternion.identity);
        Card cardComponent = card.GetComponent<Card>();
        cardComponent.LoadCardData(availableCards[cardIndex]);
        card.transform.SetParent(cardSlots[cardIndex].transform);
    }
}
