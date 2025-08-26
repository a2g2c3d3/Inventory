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
        player = new Character("유니티짱", 10, 9, "유니티짱은 UI작업이 정말 재밌다고 생각하고 있습니다. 진짜로.", 35, 40, 100, 25, 50000);

        uiManager.UIMainMenu.SetInformationUI(player);
        uiManager.UIStatus.SetStatUI(player);
    }
}
