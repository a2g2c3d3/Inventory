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
        _player = new Character("유니티짱", 1, 0, "마왕을 물리치기위해 오늘도 열심히 UI를 만들고 있는 용사 유니티짱이다.", 5, 3, 10, 1, 50000);

        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }

    public void OnBattle()
    {
        int exp = Random.Range(1, 5)+(player.Level%10);
        int gold = Random.Range(1, 10)*player.Level;
        player.AddExp(exp);
        player.AddGold(gold);
        uiManager.ShowLog($"경험치 +{exp},  {gold}골드 획득!", uiManager.OriginalColor);
        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }

    //아이템을 사면->인벤토리에 들어감->인벤토리ui를 업데이트해줌
    //GetItemLog에 획득한 아이템 이름 출력
    public void BuyItem()
    {
        int itemPrice = 2000;
        if (player.Gold < itemPrice)
        {
            uiManager.ShowLog("골드가 부족해!", Color.red);
            return;
        }
        if (itemDB.Count > 0)
        {
            int randomIndex = Random.Range(0, itemDB.Count);
            ItemData randomItem = itemDB[randomIndex];

            player.AddItem(randomItem);
            player.UseGold(itemPrice);
            uiManager.ShowLog($"{randomItem.itemName} 획득!", uiManager.OriginalColor);

            UIManager.Instance.UIMainMenu.SetInformationUI(player);
            UIManager.Instance.UIInventory.UpdateInventoryUI(player.inventory);
        }
        else
        {
            uiManager.ShowLog("어 이러면안되는데.",Color.red);
        }
    }
    public void ClearInventoryButton()
    {
        int gold = player.inventory.Count * 100;
        player.AddGold(gold);
        uiManager.ShowLog($"{gold}골드 획득!" ,uiManager.OriginalColor);
        player.inventory.Clear();
        uiManager.RefreshUI();
    }
       
    
}
