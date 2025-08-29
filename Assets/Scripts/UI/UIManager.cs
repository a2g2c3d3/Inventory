using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIStatus uiStatus; 
    [SerializeField] private UIInventory uiInventory;

    [Header("·Î±×")]
    [SerializeField] public TextMeshProUGUI logText;
    private Color _color;
    public Color OriginalColor => _color;
    private Coroutine logCoroutine;

    public UIMainMenu UIMainMenu => uiMainMenu;
    public UIStatus UIStatus => uiStatus;
    public UIInventory UIInventory => uiInventory;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        uiInventory.InitInventoryUI(100);
    }

    private void Start()
    {
        uiInventory.gameObject.SetActive(false);
        uiStatus.gameObject.SetActive(false);
        uiMainMenu.gameObject.SetActive(true);
        logText.gameObject.SetActive(false);
        _color = logText.color;
    }

    public void RefreshUI()
    {
        uiInventory.UpdateInventoryUI(GameManager.Instance.player.inventory);
        uiStatus.SetStatUI(GameManager.Instance.player);
        uiMainMenu.SetInformationUI(GameManager.Instance.player);
    }

    public void ShowLog(string message, Color color)
    {
        if (logCoroutine != null)
        {
            StopCoroutine(logCoroutine);
        }
        logCoroutine = StartCoroutine(Log(message, color));

    }

    IEnumerator Log(string message, Color color)
    {
        logText.text = message;
        logText.color = color;
        logText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        logText.gameObject.SetActive(false);
        logText.color = _color;

        logCoroutine = null;
    }

}
