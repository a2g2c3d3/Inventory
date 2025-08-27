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
        _player = new Character("����Ƽ¯", 10, 9, "����Ƽ¯�� UI�۾��� ���� ��մٰ� �����ϰ� �ֽ��ϴ�. ��¥��.", 35, 40, 100, 25, 50000);

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
            Debug.Log("���̺����ؿ�!!!!!");
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
            Debug.Log("�������� ���� �� �����ϴ�!!");
        }
    }
    //�������� ���->�κ��丮�� ��->�κ��丮ui�� ������Ʈ����
   
}
