using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Text ggyulNameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text amountText = null;
    
    [SerializeField]
    private Image ListImage = null;
    [SerializeField]
    private Sprite[] ListSprite = null;//image와 sprite 차이 image = 액자, sprite = 그림
    [SerializeField]
    private Image image, btnImage;
    [SerializeField] private Image[] challenge;
    [SerializeField] private GameObject Stamp;
    [SerializeField] private Transform canvas;

    private TaeyoungGrow taeyounggrow = null;

    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private AudioClip clip1;

    public void SetValue(TaeyoungGrow taeyounggrow)//한번만 실행 (setvalue)
    {

        this.taeyounggrow = taeyounggrow;
        if (GameManager.Instance.CurrentUser.isChallenge[taeyounggrow.ListNumber])
        {
            challenge[taeyounggrow.ListNumber].color = Color.yellow;
        }
        UpdateUI();

    }

    private void Update()
    {
        if(taeyounggrow.price > GameManager.Instance.CurrentUser.ggyul) 
        {
            image.color = Color.gray;
            btnImage.color = Color.gray;
        }
        else
        {
            btnImage.color = Color.white;
            image.color = Color.white;
        }

        if(taeyounggrow.amount > 0 && !GameManager.Instance.CurrentUser.isChallenge[taeyounggrow.ListNumber])
        {
            challenge[taeyounggrow.ListNumber].color = Color.yellow;
            SoundManager.instance.SFXPlay("TADA", clip1);
            GameManager.Instance.CurrentUser.isChallenge[taeyounggrow.ListNumber] = true;
            GameObject stamp = Instantiate(Stamp, canvas);
            
            stamp.AddComponent<AutoDestroy>();
           

        }
        
    }

    public void UpdateUI()
    {
        ggyulNameText.text = taeyounggrow.ListName;
        priceText.text = string.Format("{0} 지지율", taeyounggrow.price);
        amountText.text = string.Format("{0}", taeyounggrow.amount);
        ListImage.sprite = ListSprite[taeyounggrow.ListNumber];
    }

    public void OnclickPurchase()
    {
        if(GameManager.Instance.CurrentUser.ggyul < taeyounggrow.price)
        {
            return;
        }

        SoundManager.instance.SFXPlay("RING", clip);
        GameManager.Instance.CurrentUser.ePc += taeyounggrow.ePc;
        GameManager.Instance.CurrentUser.ggyul -= taeyounggrow.price;
        taeyounggrow.price = (long)(taeyounggrow.price * 1.25f);//쉽게 풀어서 씀
        taeyounggrow.amount++;
        GameManager.Instance.CurrentUser.ePc += 1;
        UpdateUI();
        GameManager.Instance.UI.UpdateEnergyPanel();
        
        

    }
}
