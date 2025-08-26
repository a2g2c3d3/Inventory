using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialogue : MonoBehaviour
{
    [SerializeField]
    private string[] idleDialogue;

    [SerializeField]
    private TextMeshProUGUI DialogueText;
    private void Start()
    {
        DialogueText.gameObject.SetActive(false);
    }
    public void ShowRandomDialougText()
    {
        if (idleDialogue.Length == 0) return;

        int randomIndex = Random.Range(0, idleDialogue.Length);
        string randomDialogue = idleDialogue[randomIndex]; 

        ShowDialogue(randomDialogue);
    }

    public void ShowDialogue(string dialogue)
    {
        DialogueText.gameObject.SetActive(true);
        DialogueText.text = dialogue;
    }

    public void HideDialogue()
    {
        DialogueText.gameObject.SetActive(false);
    }
}
