using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toogle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Tween _skaleTween;

    private void ClickTween(Vector3 toVector)
    {
        if (_skaleTween != null)
        {
            _skaleTween.Kill();
            _skaleTween = null;
        }

        _skaleTween = transform.DOScale(toVector, 0.1f).SetEase(Ease.InOutSine);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;

        ClickTween(new Vector3(0.9f, 0.9f, 0.9f));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ClickTween(Vector3.one);
    }
}
