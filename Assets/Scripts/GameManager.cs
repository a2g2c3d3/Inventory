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
            DontDestroyOnLoad(gameObject);
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
        _player = new Character("유니티짱", 10, 9, "유니티짱은 UI작업이 정말 재밌다고 생각하고 있습니다. 진짜로.", 35, 40, 100, 25, 50000);

        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }

    public void OnBattle()
    {
        player.AddExp(Random.Range(1,5));
        player.AddGold(Random.Range(1, 10)*player.Level);
        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }

    public void BuyItem()
    {
        int itemPrice = 2000;
        if (player.Gold < itemPrice)
        {
            Debug.Log("돈이부족해요!!!!!");
            return;
        }
        if (itemDB.Count > 0)
        {
            int randomIndex = Random.Range(0, itemDB.Count);
            ItemData randomItem = itemDB[randomIndex];

            player.AddItem(randomItem);
            player.UseGold(itemPrice);

            UIManager.Instance.UIMainMenu.SetInformationUI(player);
            UIManager.Instance.UIInventory.UpdateInventoryUI(player.inventory);
        }
        else
        {
            Debug.Log("아이템을 얻을 수 없습니다!!");
        }
    }
    //아이템을 사면->인벤토리에 들어감->인벤토리ui를 업데이트해줌
   
}
