using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public Itemtype itemType;
}

public enum Itemtype
{
    Weapon,
    Armor,
    Potion
}

// WeaponData.cs
[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class WeaponData : ItemData
{
    public float extraAtk;
}

// ArmorData.cs
[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
public class ArmorData : ItemData
{
    public float extraDef;
}

// PotionData.cs
[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Potion")]
public class PotionData : ItemData
{
    public float healAmount;
}