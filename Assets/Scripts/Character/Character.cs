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
        UIManager.Instance.ShowLog($"{item.itemName} ȹ��!", UIManager.Instance.OriginalColor);
    }
    

    // UISlot���� ȣ���ϴ� ���� �޼���
    public void ToggleEquip(InventoryItem itemToHandle)
    {
        // ����
        if (itemToHandle.itemData.itemType == ItemType.Potion)
        {
            UsePotion(itemToHandle);
            return;
        }

        // �������� �̹� ������ �������� Ȯ��
        if (equippedItems.ContainsValue(itemToHandle))
        {
            // �������̸� ����
            UnequipItem(itemToHandle);
           
        }
        else
        {
            // �ƴϸ� ����
            EquipItem(itemToHandle);
        }

        // UI ����
        UIManager.Instance.RefreshUI();
    }

    // ������ ����
    private void EquipItem(InventoryItem itemToEquip)
    {
        // 1. �̹� ���� Ÿ���� �������� �����Ǿ� �ִٸ� ����
        if (equippedItems.ContainsKey(itemToEquip.itemData.itemType))
        {
            UnequipItem(equippedItems[itemToEquip.itemData.itemType]);
        }

        // 2. �������� �����ϰ� �ɷ�ġ�� ����
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
        UIManager.Instance.ShowLog($"{itemToEquip.itemData.itemName} ����!", UIManager.Instance.OriginalColor);
    }

    // ������ ����
    private void UnequipItem(InventoryItem itemToUnequip)
    {
        // 1. ��ųʸ����� �������� �����ϰ� isEquipped = false
        if (equippedItems.ContainsValue(itemToUnequip))
        {
            equippedItems.Remove(itemToUnequip.itemData.itemType);
            itemToUnequip.isEquipped = false;
        }

        // 2. �ɷ�ġ ����
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
        UIManager.Instance.ShowLog($"{itemToUnequip.itemData.itemName} ����!", Color.red);
    }
    // �����̸�
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
        UIManager.Instance.ShowLog($"{potion.itemData.potionType} {potion.itemData.potionStat} ����!", UIManager.Instance.OriginalColor);

        // ����� ����
        inventory.Remove(potion);

        // UI ����
        UIManager.Instance.RefreshUI();
    }

}
