using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManager : MonoBehaviour
{

    [SerializeField] private GameObject[] pannel;
    //[SerializeField]
    //private GameObject pannel1;
    [SerializeField] private bool[] isClick;
    [SerializeField] private int len;
    

    public void Activation(GameObject obj)
    {
        obj.SetActive(true);

    }
    public void UnActivation(GameObject obj)
    {
        obj.SetActive(false);

    }

    public void Onclick(int index)
    {
        //isClick[index] = !isClick[index];
        if (isClick[index] == false)//받은값의 isclick 이 false 일때
        {
            for(int i = 0;i < len; i++)//0부터 len 까지 판넬 초기화
            {
                pannel[i].SetActive(false);
            }
            for (int i = 0; i < len; i++)//0부터 len 까지 isClick 초기화
            {
                isClick[i] = false;
            }
            pannel[index].SetActive(true);//받은 값의 판넬 보이기
            isClick[index] = true;//받은값의 isclick 켜주기
        }
        else//true 일때 (두번 연속 클릭)
        {
            //for (int i = 0; i < len; i++)//0부터 len 까지 판넬 초기화
            //{
            //    pannel[i].SetActive(false);
            //}
            pannel[index].SetActive(false);//받은 값의 판넬을 꺼주는것
            isClick[index] = false;//받은값의 isclick 꺼주기
        }
    }

    


}
