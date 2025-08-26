using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator anim;
    public float blinkColldown = 3f;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(BlinkLoop());
    }

    private IEnumerator BlinkLoop()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(blinkColldown); //while�� �ۿ��� ĳ��
        while (true)
        {
            yield return waitForSeconds;

            anim.SetTrigger("Blink");

        }
    }


}
