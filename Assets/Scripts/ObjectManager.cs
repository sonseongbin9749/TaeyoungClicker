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
        if (isClick[index] == false)//�������� isclick �� false �϶�
        {
            for(int i = 0;i < len; i++)//0���� len ���� �ǳ� �ʱ�ȭ
            {
                pannel[i].SetActive(false);
            }
            for (int i = 0; i < len; i++)//0���� len ���� isClick �ʱ�ȭ
            {
                isClick[i] = false;
            }
            pannel[index].SetActive(true);//���� ���� �ǳ� ���̱�
            isClick[index] = true;//�������� isclick ���ֱ�
        }
        else//true �϶� (�ι� ���� Ŭ��)
        {
            //for (int i = 0; i < len; i++)//0���� len ���� �ǳ� �ʱ�ȭ
            //{
            //    pannel[i].SetActive(false);
            //}
            pannel[index].SetActive(false);//���� ���� �ǳ��� ���ִ°�
            isClick[index] = false;//�������� isclick ���ֱ�
        }
    }

    


}
