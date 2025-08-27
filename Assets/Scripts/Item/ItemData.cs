using UnityEngine;

public enum Itemtype
{
    Weapon,
    Armor,
    Potion
}

[CreateAssetMenu(fileName ="New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public Itemtype itemType;

    //公扁
    public int extraAtk;
    //规绢备
    public int extraDef;
    //器记
    public int healAmount;
}


