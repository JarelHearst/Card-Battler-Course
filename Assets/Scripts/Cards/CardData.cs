using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public Sprite illustration;
    
    public Transform cardVisual;

    public string cardName;

    public string description;

    public int actionCost;

    public int attackPower;

    public int healPower;

    public bool isDoubleAttack;

}


