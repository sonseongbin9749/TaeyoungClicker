using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text energyText = null;
    [SerializeField]
    private Text donateText = null;
    [SerializeField]
    private Animator beakerAnimator = null;
    [SerializeField]
    private GameObject upgradePanelTemplate = null;
    [SerializeField]
    private Energytext energytextTemplate = null;
    [SerializeField]
    private DonateText donateTextTemplate = null;
    [SerializeField]
    private Transform pool = null;

    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>(); 

    private void Start()
    {
        UpdateEnergyPanel();
        CreatePanel();  
    }

    private void CreatePanel()
    {
        int index = 0;
        GameObject newPanel = null;
        UpgradePanel newPanelComponent = null;
        foreach(TaeyoungGrow soldier in GameManager.Instance.CurrentUser.taeyounglist)
        {
            newPanel = Instantiate(upgradePanelTemplate, upgradePanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgradePanel>();
            newPanelComponent.SetValue(soldier);
            newPanel.SetActive(true);
            upgradePanelList.Add(newPanelComponent);
            GameManager.Instance.upg[index] = newPanelComponent;
            index++;
        }
    }

    public void OnClickTaeyoung()
    {
        int a = Random.Range(0, 80);
        if(a == 5)
        {
            Debug.Log("보석!");
            GameManager.Instance.CurrentUser.donate++;
            UpdateEnergyPanel();
            beakerAnimator.Play("Dance");
            DonateText newText = null;
            newText = Instantiate(donateTextTemplate, donateTextTemplate.transform.parent);
            newText.Show(Input.mousePosition);
        }
        else
        {
            GameManager.Instance.CurrentUser.ggyul += GameManager.Instance.CurrentUser.ePc;
            UpdateEnergyPanel();
            beakerAnimator.Play("Dance");
            //클릭 풀링 설정
            Energytext newText = null;

            if (pool.childCount > 0)
            {
                newText = pool.GetChild(0).GetComponent<Energytext>();
            }
            else
            {
                newText = Instantiate(energytextTemplate, energytextTemplate.transform.parent);
            }
            newText.Show(Input.mousePosition);
        }
    }
    public void OnClickTaeyoungAuto()
    {
        GameManager.Instance.CurrentUser.ggyul += GameManager.Instance.CurrentUser.ePc * GameManager.Instance.Boost;
        UpdateEnergyPanel();
        beakerAnimator.Play("Dance");
        //클릭 풀링 설정
        Energytext newText = null;

        if (pool.childCount > 0)
        {
            newText = pool.GetChild(0).GetComponent<Energytext>();
        }
        else
        {
            newText = Instantiate(energytextTemplate, energytextTemplate.transform.parent);
        }

        newText.Show(new Vector2(700,1500));


    }
    public void UpdateEnergyPanel()
    {
        energyText.text = string.Format("{0} 지지율", GameManager.Instance.CurrentUser.ggyul * GameManager.Instance.Boost);
        donateText.text = string.Format("{0} 후원", GameManager.Instance.CurrentUser.donate);
    }
}

