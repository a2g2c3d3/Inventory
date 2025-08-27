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
        Debug.Log($"{item}�� ȹ�� �Ͽ����ϴ�.");
    }
    

    // UISlot���� ȣ���ϴ� ���� �޼���
    public void ToggleEquip(InventoryItem itemToHandle)
    {
        // ������ ����/���� ����� �ƴմϴ�.
        if (itemToHandle.itemData.itemType == ItemType.Potion)
        {
            UsePotion(itemToHandle);
            return;
        }

        // �������� �̹� ������ �������� Ȯ���մϴ�.
        if (equippedItems.ContainsValue(itemToHandle))
        {
            // �̹� ������ �������̶�� �����մϴ�.
            UnequipItem(itemToHandle);
        }
        else
        {
            // ���� �������� ���� �������̶�� �����մϴ�.
            EquipItem(itemToHandle);
        }

        // UI ��ü�� �����մϴ�.
        RefreshUI();
    }

    // �������� �����ϴ� �޼��� (���� ȣ���)
    private void EquipItem(InventoryItem itemToEquip)
    {
        // 1. �̹� ���� Ÿ���� �������� �����Ǿ� �ִٸ� ���� �����մϴ�.
        if (equippedItems.ContainsKey(itemToEquip.itemData.itemType))
        {
            UnequipItem(equippedItems[itemToEquip.itemData.itemType]);
        }

        // 2. �� �������� �����ϰ� �ɷ�ġ�� �����մϴ�.
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

    // �������� �����ϴ� �޼��� (���� ȣ���)
    private void UnequipItem(InventoryItem itemToUnequip)
    {
        // 1. ��ųʸ����� �������� �����ϰ� isEquipped ���¸� false�� �����մϴ�.
        if (equippedItems.ContainsValue(itemToUnequip))
        {
            equippedItems.Remove(itemToUnequip.itemData.itemType);
            itemToUnequip.isEquipped = false;
        }

        // 2. �ɷ�ġ�� �ǵ����ϴ�.
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
    // ���� �������� ����ϴ� �޼���
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

        // ������ ��������� �κ��丮���� �����մϴ�.
        inventory.Remove(potion);

        // UI�� �����մϴ�.
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
    //    // ��ųʸ��� �̹� ������ ���������� Ȯ���մϴ�.
    //    if (equippedItems.ContainsValue(item))
    //    {
    //        // �̹� ������ �������̶�� ���� ������ �����մϴ�.
    //        UnequipItem(item);
    //    }
    //    else
    //    {
    //        // ���� �������� ���� �������̶�� ���� ������ �����մϴ�.
    //        EquipItem(item);
    //    }

    //    // ������ �κ��丮�� ���¸� �����մϴ�.
    //    inventory.Sort((item1, item2) => item2.isEquipped.CompareTo(item1.isEquipped));

    //    // UI ��ü�� �����մϴ�.
    //    UIManager.Instance.UIInventory.UpdateInventoryUI(inventory);
    //    UIManager.Instance.UIStatus.SetStatUI(GameManager.Instance.player);
    //}

    //// �������� �����ϴ� �޼���
    //private void EquipItem(InventoryItem item)
    //{
    //    // ���� Ÿ���� �������� �̹� �����Ǿ� �ִٸ� ���� �����մϴ�.
    //    if (equippedItems.ContainsKey(item.itemData.itemType))
    //    {
    //        UnequipItem(equippedItems[item.itemData.itemType]);
    //    }

    //    // �������� ��ųʸ��� �߰��ϰ� isEquipped ���¸� true�� �����մϴ�.
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

    //// �������� �����ϴ� �޼���
    //private void UnequipItem(InventoryItem item)
    //{
    //    // ��ųʸ����� �������� �����ϰ� isEquipped ���¸� false�� �����մϴ�.
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


