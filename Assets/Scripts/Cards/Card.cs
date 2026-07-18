using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

[RequireComponent(typeof(SortingGroup))]
public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer illustrationRender;

    [SerializeField] private SpriteRenderer glowOverlay;

    [SerializeField] private TextMeshPro cardNameText;

    [SerializeField] private TextMeshPro descriptionText;

    [SerializeField] private TextMeshPro actionsText;

    [SerializeField] private Transform cardVisual;

    [SerializeField] private float hoverScale = 2f;

    [SerializeField] private float hoverOffset = 2f;

    [SerializeField] private float glowDuration = .5f;

    private Vector3 originalVisualScale;

    private Vector3 originalCardPosition;

    private Vector3 originalVisualPosition;

    private SortingGroup sortingGroup;

    private CardData cardData;

    private Collider2D cardCollider;

    private int originalSortingOrder;

    private static bool isBeingDragged = false;

    private bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {   
        originalVisualScale = cardVisual.localScale;
        originalCardPosition = transform.localPosition;
        originalVisualPosition = cardVisual.localPosition;


        originalSortingOrder = sortingGroup.sortingOrder;
    }

    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
        cardCollider = GetComponent<Collider2D>();
    }

    public void LoadCardData(CardData cardData)
    {
        this.cardData = cardData;
        illustrationRender.sprite = cardData.illustration;
        cardNameText.text = cardData.cardName;
        descriptionText.text = cardData.description;
        actionsText.text = cardData.actionCost.ToString();
    }

    private void OnMouseEnter()
    {
        Debug.Log($"Entered: {gameObject.name}");

        if (isBeingDragged)
        {
            return;
        }

        cardVisual.localScale = originalVisualScale * hoverScale;
        cardVisual.localPosition = originalVisualPosition + new Vector3(0f, hoverOffset, 0f);

        sortingGroup.sortingOrder = originalSortingOrder + 1;
    }

    private void OnMouseExit()
    {
        Debug.Log($"Exited: {gameObject.name}");
        if(isBeingDragged)
        {
            return;
        }
        
        ResetCardVisual();
    }

    private void OnMouseDrag()
    {
        if (!isBeingDragged)
        {
            ResetCardVisual();
        }

        isBeingDragged = true;
        gameObject.transform.position = GetMousePosition();
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void SetIsPlaying(bool playing)
    {
        isPlaying = playing;
    }

    private void OnMouseUp()
    {
        isBeingDragged = false;
        
        if(isPlaying)
        {
            return;
        }

        ResetCardVisual();

        transform.localPosition = originalCardPosition;
    }

    public CardData GetCardData() => cardData;

    private void OnDestroy()
    {
        isBeingDragged = false;
    }
    
    public void SetInteractable(bool interactable)
    {
        cardCollider.enabled = interactable;
    }

    private void ResetCardVisual()
    {
        cardVisual.localScale = originalVisualScale;
        cardVisual.localPosition = originalVisualPosition;
        sortingGroup.sortingOrder = originalSortingOrder;
    }

    public void Glow()
    {
        StartCoroutine(GlowCoroutine());
    }

    private IEnumerator GlowCoroutine()
    {
        glowOverlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(glowDuration);
        glowOverlay.gameObject.SetActive(false);
    }
}
