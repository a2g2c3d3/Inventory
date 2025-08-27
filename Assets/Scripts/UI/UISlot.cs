using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Image equipMark;
    [SerializeField] private InventoryItem _item;

    private void Start()
    {
        CheckEquipMark();
    }

    public void SetItem(InventoryItem item)
    {
        _item = item;
        itemImage.enabled = true;
        itemImage.sprite = _item.itemData.itemIcon;
    }
    public void Update()
    {
        CheckEquipMark();
    }
    

    public void OnEquipButton()
    {
        GameManager.Instance.player.ToggleEquip(_item);
        CheckEquipMark();
    }

    public void CheckEquipMark()
    {
        if (_item.isEquipped)
        {
            equipMark.gameObject.SetActive(true);
        }
        else
        {
            equipMark.gameObject.SetActive(false);
        }
    }
}
