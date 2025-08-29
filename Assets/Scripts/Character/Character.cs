using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string ID { get; private set; }
    public int Level { get; private set; }
    public float Exp { get; private set; }

    public float maxExp => Level + 2;
    public string Description { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Hp { get; private set; }
    public int Cri { get; private set; }
    public int Gold { get; private set; }

    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemType, InventoryItem> equippedItems = new Dictionary<ItemType, InventoryItem>();

    public Character(string id, int level, float exp, string description, int atk, int def, int hp, int cri, int gold)
    {
        ID = id;
        Level = level;
        Exp = exp;
        Description = description;
        Atk = atk;
        Def = def;
        Hp = hp;
        Cri = cri;
        Gold = gold;
    }

    public void AddExp(int amount)
    {
        Exp += amount;
        if (Exp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Exp -= maxExp;
        Level++;
        AddAtk(Level / 5);
        AddDef(Level / 8);
        AddHP(Level / 10);
        AddCri(Level / 20);
    }

    public void AddAtk(int extraAtk)
    {
        Atk += extraAtk;
    }

    public void AddDef(int extraDef)
    {
        Def += extraDef;
    }
    public void AddHP(int extraHp)
    {
        Hp += extraHp;
    }
    public void AddCri(int extraCri)
    {
        Cri += extraCri;
    }
    public void AddGold(int gold)
    {
        Gold += gold;
    }
    public void UseGold(int gold)
    {
        Gold -= gold;
    }

    public void AddItem(ItemData item)
    {
        inventory.Add(new InventoryItem(item));
        UIManager.Instance.ShowLog($"{item.itemName} 획득!", UIManager.Instance.OriginalColor);
    }
    

    // UISlot에서 호출하는 통합 메서드
    public void ToggleEquip(InventoryItem itemToHandle)
    {
        // 포션
        if (itemToHandle.itemData.itemType == ItemType.Potion)
        {
            UsePotion(itemToHandle);
            return;
        }

        // 아이템이 이미 장착된 상태인지 확인
        if (equippedItems.ContainsValue(itemToHandle))
        {
            // 장착중이면 해제
            UnequipItem(itemToHandle);
           
        }
        else
        {
            // 아니면 장착
            EquipItem(itemToHandle);
        }

        // UI 갱신
        UIManager.Instance.RefreshUI();
    }

    // 아이템 장착
    private void EquipItem(InventoryItem itemToEquip)
    {
        // 1. 이미 같은 타입의 아이템이 장착되어 있다면 해제
        if (equippedItems.ContainsKey(itemToEquip.itemData.itemType))
        {
            UnequipItem(equippedItems[itemToEquip.itemData.itemType]);
        }

        // 2. 아이템을 장착하고 능력치를 적용
        equippedItems[itemToEquip.itemData.itemType] = itemToEquip;
        itemToEquip.isEquipped = true;

        switch (itemToEquip.itemData.itemType)
        {
            case ItemType.Weapon:
                AddAtk(itemToEquip.itemData.itemStat);
                break;
            case ItemType.Shield:
            case ItemType.Armor:
            case ItemType.Helmet:
                AddDef(itemToEquip.itemData.itemStat);
                break;
        }
        UIManager.Instance.ShowLog($"{itemToEquip.itemData.itemName} 장착!", UIManager.Instance.OriginalColor);
    }

    // 아이템 해제
    private void UnequipItem(InventoryItem itemToUnequip)
    {
        // 1. 딕셔너리에서 아이템을 제거하고 isEquipped = false
        if (equippedItems.ContainsValue(itemToUnequip))
        {
            equippedItems.Remove(itemToUnequip.itemData.itemType);
            itemToUnequip.isEquipped = false;
        }

        // 2. 능력치 복구
        switch (itemToUnequip.itemData.itemType)
        {
            case ItemType.Weapon:
                AddAtk(-itemToUnequip.itemData.itemStat);
                break;
            case ItemType.Shield:
            case ItemType.Armor:
            case ItemType.Helmet:
                AddDef(-itemToUnequip.itemData.itemStat);
                break;
        }
        UIManager.Instance.ShowLog($"{itemToUnequip.itemData.itemName} 해제!", Color.red);
    }
    // 포션이면
    public void UsePotion(InventoryItem potion)
    {
        switch (potion.itemData.potionType)
        {
            case PotionType.HP:
                GameManager.Instance.player.AddHP(potion.itemData.potionStat);
                break;

            case PotionType.Exp:
                GameManager.Instance.player.AddExp(potion.itemData.potionStat);
                break;

            case PotionType.Atk:
                GameManager.Instance.player.AddAtk(potion.itemData.potionStat);
                break;

            case PotionType.Def:
                GameManager.Instance.player.AddDef(potion.itemData.potionStat);
                break;
        }
        UIManager.Instance.ShowLog($"{potion.itemData.potionType} {potion.itemData.potionStat} 증가!", UIManager.Instance.OriginalColor);

        // 사용후 제거
        inventory.Remove(potion);

        // UI 갱신
        UIManager.Instance.RefreshUI();
    }

}
