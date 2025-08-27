using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Shield, 
    Helmet, 
    Potion
}

public enum PotionType
{
    Atk,
    Def,
    HP,
    Exp,
    Cri

}

[CreateAssetMenu(fileName ="New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;

    public int itemStat;

    public PotionType potionType;
    public int potionStat;
}


