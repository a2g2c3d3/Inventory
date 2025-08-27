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
        Debug.Log($"{item}을 획득 하였습니다.");
    }
    

    // UISlot에서 호출하는 통합 메서드
    public void ToggleEquip(InventoryItem itemToHandle)
    {
        // 포션은 장착/해제 대상이 아닙니다.
        if (itemToHandle.itemData.itemType == ItemType.Potion)
        {
            UsePotion(itemToHandle);
            return;
        }

        // 아이템이 이미 장착된 상태인지 확인합니다.
        if (equippedItems.ContainsValue(itemToHandle))
        {
            // 이미 장착된 아이템이라면 해제합니다.
            UnequipItem(itemToHandle);
        }
        else
        {
            // 아직 장착되지 않은 아이템이라면 장착합니다.
            EquipItem(itemToHandle);
        }

        // UI 전체를 갱신합니다.
        RefreshUI();
    }

    // 아이템을 장착하는 메서드 (내부 호출용)
    private void EquipItem(InventoryItem itemToEquip)
    {
        // 1. 이미 같은 타입의 아이템이 장착되어 있다면 먼저 해제합니다.
        if (equippedItems.ContainsKey(itemToEquip.itemData.itemType))
        {
            UnequipItem(equippedItems[itemToEquip.itemData.itemType]);
        }

        // 2. 새 아이템을 장착하고 능력치를 적용합니다.
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
    }

    // 아이템을 해제하는 메서드 (내부 호출용)
    private void UnequipItem(InventoryItem itemToUnequip)
    {
        // 1. 딕셔너리에서 아이템을 제거하고 isEquipped 상태를 false로 변경합니다.
        if (equippedItems.ContainsValue(itemToUnequip))
        {
            equippedItems.Remove(itemToUnequip.itemData.itemType);
            itemToUnequip.isEquipped = false;
        }

        // 2. 능력치를 되돌립니다.
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
    }
    // 포션 아이템을 사용하는 메서드
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

        // 포션을 사용했으니 인벤토리에서 제거합니다.
        inventory.Remove(potion);

        // UI를 갱신합니다.
        RefreshUI();
    }

    public void RefreshUI()
    {
        UIManager.Instance.UIInventory.UpdateInventoryUI(inventory);
        UIManager.Instance.UIStatus.SetStatUI(GameManager.Instance.player);
        UIManager.Instance.UIMainMenu.SetInformationUI(GameManager.Instance.player);
    }
}
  

    //private Dictionary<ItemType, InventoryItem> equippedItems = new Dictionary<ItemType, InventoryItem>();
    //public void ToggleEquip(InventoryItem item)
    //{
    //    // 딕셔너리에 이미 장착된 아이템인지 확인합니다.
    //    if (equippedItems.ContainsValue(item))
    //    {
    //        // 이미 장착된 아이템이라면 해제 로직을 실행합니다.
    //        UnequipItem(item);
    //    }
    //    else
    //    {
    //        // 아직 장착되지 않은 아이템이라면 장착 로직을 실행합니다.
    //        EquipItem(item);
    //    }

    //    // 아이템 인벤토리의 상태를 정렬합니다.
    //    inventory.Sort((item1, item2) => item2.isEquipped.CompareTo(item1.isEquipped));

    //    // UI 전체를 갱신합니다.
    //    UIManager.Instance.UIInventory.UpdateInventoryUI(inventory);
    //    UIManager.Instance.UIStatus.SetStatUI(GameManager.Instance.player);
    //}

    //// 아이템을 장착하는 메서드
    //private void EquipItem(InventoryItem item)
    //{
    //    // 같은 타입의 아이템이 이미 장착되어 있다면 먼저 해제합니다.
    //    if (equippedItems.ContainsKey(item.itemData.itemType))
    //    {
    //        UnequipItem(equippedItems[item.itemData.itemType]);
    //    }

    //    // 아이템을 딕셔너리에 추가하고 isEquipped 상태를 true로 변경합니다.
    //    equippedItems[item.itemData.itemType] = item;
    //    item.isEquipped = true;

    //    switch (item.itemData.itemType)
    //    {
    //        case ItemType.Weapon:
    //            AddAtk(item.itemData.itemStat);
    //            break;
    //        case ItemType.Shield:
    //        case ItemType.Armor:
    //        case ItemType.Helmet:
    //            AddDef(item.itemData.itemStat);
    //            break;
    //    }
    //}

    //// 아이템을 해제하는 메서드
    //private void UnequipItem(InventoryItem item)
    //{
    //    // 딕셔너리에서 아이템을 제거하고 isEquipped 상태를 false로 변경합니다.
    //    equippedItems.Remove(item.itemData.itemType);
    //    item.isEquipped = false;

    //    switch (item.itemData.itemType)
    //    {
    //        case ItemType.Weapon:
    //            AddAtk(-item.itemData.itemStat);
    //            break;
    //        case ItemType.Shield:
    //        case ItemType.Armor:
    //        case ItemType.Helmet:
    //            AddDef(-item.itemData.itemStat);
    //            break;
    //    }
    //}
    //public void EquiptItem(InventoryItem item)
    //{
    //    if (item.isEquipped)
    //    {
    //        switch (item.itemData.itemType)
    //        {
    //            case ItemType.Weapon:
    //                GameManager.Instance.player.AddAtk(item.itemData.itemStat);
    //                break;
    //            case ItemType.Armor:
    //            case ItemType.Helmet:
    //            case ItemType.Shield:
    //                GameManager.Instance.player.AddDef(item.itemData.itemStat);
    //                break;
    //        }
    //        UIManager.Instance.UIStatus.SetStatUI(GameManager.Instance.player);
    //    }
    //    else 
    //    {
    //        switch (item.itemData.itemType)
    //        {
    //            case ItemType.Weapon:
    //                GameManager.Instance.player.AddAtk(-item.itemData.itemStat);
    //                break;
    //            case ItemType.Armor:
    //            case ItemType.Helmet:
    //            case ItemType.Shield:
    //                GameManager.Instance.player.AddDef(-item.itemData.itemStat);
    //                break;
    //        }
    //        UIManager.Instance.UIStatus.SetStatUI(GameManager.Instance.player);
    //    }


    //}


