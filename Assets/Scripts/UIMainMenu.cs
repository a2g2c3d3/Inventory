using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIMainMenu : MonoBehaviour
{
    [Header("UI버튼")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button backButton;

    [Header("캐릭터 정보")]
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Image expBar;


    private bool isStatus;
    private bool isInventory;

    private void Start()
    {
        statusButton.onClick.AddListener(OpenStatus);
        inventoryButton.onClick.AddListener(OpenInventory);
        backButton.onClick.AddListener(OpenMainMenu);

        backButton.gameObject.SetActive(false);
    }
    public void OpenMainMenu()
    {
        if (isInventory)
        {
            UIManager.Instance.UIInventory.gameObject.SetActive(false);
            isInventory = false;
        }
        else if(isStatus) 
        {
            UIManager.Instance.UIStatus.gameObject.SetActive(false);
            isStatus = false;
        }
        statusButton.gameObject.SetActive(true);
        inventoryButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }
    public void OpenStatus()
    {
        isStatus = true;
        statusButton.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
        UIManager.Instance.UIStatus.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
    public void OpenInventory()
    {
        isInventory = true;
        statusButton.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
        UIManager.Instance.UIInventory.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void SetInformationUI(Character character)
    {
        idText.text = $"{character.ID}";
        levelText.text = $"Lv {character.Level}";
        expText.text = $"{character.Exp} / {character.Level + 2}";
        float expGuage = character.Exp / character.maxExp;
        expBar.fillAmount = expGuage;
        descriptionText.text = $"{character.Description}";
        goldText.text = $"{character.Gold}";
    }
}
