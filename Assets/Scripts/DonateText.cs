using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DonateText : Energytext
{
    public override void Show(Vector2 mousePosition)
    {
        energytext = GetComponent<Text>();
        energytext.text = string.Format("+1 ÈÄ¿ø");

        transform.SetParent(canvas.transform);

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        gameObject.SetActive(true);

        RectTransform rectTransform = GetComponent<RectTransform>();

        float targetPositionY = rectTransform.anchoredPosition.y + 30;

        energytext.DOFade(0f, 1.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }
}
