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
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ �ı����� �ʰ� �մϴ�.
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������ �� ��ü�� �ı��մϴ�.
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
