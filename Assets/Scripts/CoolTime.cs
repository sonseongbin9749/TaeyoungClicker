using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoolTime : MonoBehaviour
{
    private float timer;
    private int maxTime;
    Image image;
    bool isTiming;
    public void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;
            image.fillAmount = timer / maxTime;
            if (timer < 0)
            {
                //image.gameObject.SetActive(false);
                isTiming = false;
            }
        }
    }
    public void StartCoolTime(int maxTimeCur, Image imageCur)
    {
        //image.gameObject.SetActive(true);
        image = imageCur;
        maxTime = maxTimeCur;
        timer = maxTime;
        isTiming = true;
    }
}
