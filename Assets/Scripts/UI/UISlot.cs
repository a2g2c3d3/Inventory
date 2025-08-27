using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private bool isEquip;

    public void SetItem(ItemData itemData)
    {
        itemImage.enabled = true;
        itemImage.sprite = itemData.itemIcon;
    }

    public void RefreshUI()
    {
        itemImage.enabled = false;
        itemImage.sprite = null;
    }
}
