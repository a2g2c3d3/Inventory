using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Character _player;
    public Character player => _player;

    public List<ItemData> itemDB;

    [SerializeField] private UIManager uiManager;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        itemDB = new List<ItemData>(Resources.LoadAll<ItemData>("Items"));
    }

    void Start()
    {
        SetPlayerData();
    }
    public void SetPlayerData()
    {
        _player = new Character("����Ƽ¯", 1, 0, "������ ����ġ������ ���õ� ������ UI�� ����� �ִ� ��� ����Ƽ¯�̴�.", 5, 3, 10, 1, 50000);

        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }

    public void OnBattle()
    {
        int exp = Random.Range(1, 5)+(player.Level%10);
        int gold = Random.Range(1, 10)*player.Level;
        player.AddExp(exp);
        player.AddGold(gold);
        uiManager.ShowLog($"����ġ +{exp},  {gold}��� ȹ��!", uiManager.OriginalColor);
        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }

    //�������� ���->�κ��丮�� ��->�κ��丮ui�� ������Ʈ����
    //GetItemLog�� ȹ���� ������ �̸� ���
    public void BuyItem()
    {
        int itemPrice = 2000;
        if (player.Gold < itemPrice)
        {
            uiManager.ShowLog("��尡 ������!", Color.red);
            return;
        }
        if (itemDB.Count > 0)
        {
            int randomIndex = Random.Range(0, itemDB.Count);
            ItemData randomItem = itemDB[randomIndex];

            player.AddItem(randomItem);
            player.UseGold(itemPrice);
            uiManager.ShowLog($"{randomItem.itemName} ȹ��!", uiManager.OriginalColor);

            UIManager.Instance.UIMainMenu.SetInformationUI(player);
            UIManager.Instance.UIInventory.UpdateInventoryUI(player.inventory);
        }
        else
        {
            uiManager.ShowLog("�� �̷���ȵǴµ�.",Color.red);
        }
    }
    public void ClearInventoryButton()
    {
        int gold = player.inventory.Count * 100;
        player.AddGold(gold);
        uiManager.ShowLog($"{gold}��� ȹ��!" ,uiManager.OriginalColor);
        player.inventory.Clear();
        uiManager.RefreshUI();
    }
       
    
}
