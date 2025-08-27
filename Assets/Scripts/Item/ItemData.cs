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

    //����
    public int extraAtk;
    //��
    public int extraDef;
    //����
    public int healAmount;
}


