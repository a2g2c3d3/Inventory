using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private Animator anim;
    public float blinkColldown;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(BlinkLoop());
    }

    private IEnumerator BlinkLoop()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(blinkColldown); //while문 밖에서 캐싱
        while (true)
        {
            yield return waitForSeconds;

            anim.SetTrigger("Blink");

        }
    }


}
