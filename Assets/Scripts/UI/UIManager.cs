using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIStatus uiStatus; 
    [SerializeField] private UIInventory uiInventory;

    public UIMainMenu UIMainMenu => uiMainMenu;
    public UIStatus UIStatus => uiStatus;
    public UIInventory UIInventory => uiInventory;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않게 합니다.
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성된 이 객체를 파괴합니다.
        }
        uiInventory.InitInventoryUI(100);
    }

    private void Start()
    {
        uiInventory.gameObject.SetActive(false);
        uiStatus.gameObject.SetActive(false);
        uiMainMenu.gameObject.SetActive(true);
    }


}
