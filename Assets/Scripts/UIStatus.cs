using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
    [Header("캐릭터 능력치")]
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI criText;

    public void SetStatUI(Character character)
    {
        atkText.text = $"{character.Atk}";
        defText.text = $"{character.Def}";
        hpText.text = $"{character.Hp}";
        criText.text = $"{character.Cri}";
    }
}
