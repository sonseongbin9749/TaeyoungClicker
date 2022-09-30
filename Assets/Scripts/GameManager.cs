using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : Monosingleton<GameManager>
{
    private string SAVE_PATH = "";

    private string SAVE_FILENAME = "/SaveFile.txt";

    [SerializeField]
    public UpgradePanel[] upg;

    [SerializeField]
    private User user = null;

    private int item = 1;

    private int boostEPC = 1;

    public int Boost { get { return boostEPC; } }

    [SerializeField] private Image[] itemImage, itembuttons;

    [SerializeField] private int[] itemPrice;

    public User CurrentUser { get { return user; } } //유저정보 꺼내와버리기

    private UIManager uIManager;

    public UIManager UI { get { return uIManager; } }

    [SerializeField] bool[] isUsingItem;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private AudioSource music;

    [SerializeField]
    private CoolTime[] cool;

    [SerializeField]
    private Image[] coolTimeImages;

    [SerializeField]
    private GameObject Menuset;

    


    private void Awake()
    {
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson", 1f, 3f);
        InvokeRepeating("EarnEnergyPerSecond", 0f, 1f);
        InvokeRepeating("AutoClick", 0f, 0.3f);
        LoadFromJson();
        uIManager = GetComponent<UIManager>();
    }


    void Update()
    {
        for(int i = 0; i < 3; i++)
        {
            if (user.donate < itemPrice[i] || isUsingItem[i])
            {
                itemImage[i].color = Color.gray;
                itembuttons[i].color = Color.gray;
            }
            else
            {
                itemImage[i].color = Color.white;
                itembuttons[i].color = Color.white;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            Menuset.SetActive(true);
            Time.timeScale = 0;
            music.Pause();

            
        }
    }
    private void EarnEnergyPerSecond()//초당에너지 벌기
    {
        foreach(TaeyoungGrow taeyounggrow in user.taeyounglist)
        {
            user.ggyul += taeyounggrow.ePs * taeyounggrow.amount * item;
        }
        UI.UpdateEnergyPanel();
    }

    private void AutoClick()
    {
        if (isUsingItem[1])
        {
            UI.OnClickTaeyoungAuto();
        }
        UI.UpdateEnergyPanel();
    }
    public void OnClickBoostEPC(int price)
    {
        if (!isUsingItem[2] && price <= user.donate)
        {
            user.donate -= price;
            isUsingItem[2] = true;
            boostEPC = 2;
            SoundManager.instance.SFXPlay("RING2", clip);
            Invoke("RestItem2", 60);
            cool[0].StartCoolTime(60, coolTimeImages[0]);
        }
    }
    public void OnClickBoostEPS(int price)
    {
        if (!isUsingItem[0] && price <= user.donate)
        {
            user.donate -= price;
            item = 2;
            isUsingItem[0] = true;
            SoundManager.instance.SFXPlay("RING2", clip);
            Invoke("ResetItem", 30f);
            cool[1].StartCoolTime(30, coolTimeImages[1]);
            
        }
    }
    public void OnClickAuto(int price)
    {
        if (!isUsingItem[1] && price <= user.donate)
        {
            user.donate -= price;
            isUsingItem[1] = true;
            SoundManager.instance.SFXPlay("RING2", clip);
            Invoke("RestItem1", 300f);
            cool[2].StartCoolTime(300, coolTimeImages[2]);
            
        }

     }

void ResetItem()
    {
        item = 1;
        isUsingItem[0] = false;
    }
    
    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
            
        }
        if(user.istuto == false)//튜토리얼 실행
        {
            user.istuto = true;
            SaveToJson();
        }
    }
    private void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
        
    }

    public void Resetll()
    {
        int index = 0;
        user.ggyul = 0;
        user.ePc = 1;
        user.donate = 0;
        for(int i = 0; i < 4; i++)
        {
            user.isChallenge[i] = false;
        }
        foreach (TaeyoungGrow soldier in user.taeyounglist)
        {
            soldier.amount = 0;
        
            soldier.price = soldier.basicPrice;
            upg[index].UpdateUI();
            index++;
        }
        UI.UpdateEnergyPanel();
        SaveToJson();
        UI.UpdateEnergyPanel();
    }

    public void OnClickContinue()
    {
        Menuset.SetActive(false);
        Time.timeScale = 1;
        music.Play();
    }

    public void OnClickOut()
    {
        Application.Quit();
    }

    public void OnApplicationQuit()
    {
        SaveToJson();
        
    }


}