using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Energytext : MonoBehaviour
{

    protected Text energytext = null;


    [SerializeField]
    protected Canvas canvas = null;
    [SerializeField]
    private Transform pool = null;

    public virtual void Show(Vector2 mousePosition)
    {
        energytext = GetComponent<Text>();
        energytext.text = string.Format("+{0} 지지율 확보", (GameManager.Instance.CurrentUser.ePc * GameManager.Instance.Boost));

        transform.SetParent(canvas.transform);

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        gameObject.SetActive(true);

        RectTransform rectTransform = GetComponent<RectTransform>();

        float targetPositionY = rectTransform.anchoredPosition.y + 400;

        energytext.DOFade(0f, 1.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }

    public virtual void Despawn()
    {
        energytext.DOFade(1f, 0f);
        transform.SetParent(pool);
        gameObject.SetActive(false);
    }

    
    
}
