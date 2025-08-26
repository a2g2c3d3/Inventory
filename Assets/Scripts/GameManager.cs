using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Character player;

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
    }
    void Start()
    {
        SetData();
    }

    public void test()
    {
        player.addExp(Random.Range(1,5));
        player.AddGold(Random.Range(1, 10));
        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }
    public void SetData()
    {
        player = new Character("����Ƽ¯", 10, 9, "����Ƽ¯�� UI�۾��� ���� ��մٰ� �����ϰ� �ֽ��ϴ�. ��¥��.", 35, 40, 100, 25, 50000);

        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }
}
