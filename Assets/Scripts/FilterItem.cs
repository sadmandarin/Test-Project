using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FilterItem : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _dotImage;
    [SerializeField] private Image _outline;

    [Space]
    [SerializeField] private Vector3 _scaleSize = new(0.9f, 0.9f, 0.9f);

    private Tween _scaleTween;
    private bool _dotImageEnabledOnStart;

    private void OnEnable()
    {
        _dotImageEnabledOnStart = _dotImage.enabled;
        _outline.enabled = _toggle.isOn;

        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveAllListeners();
    }

    private void OnToggleValueChanged(bool isToggleActive)
    {
        if (_dotImageEnabledOnStart)
        {
            _dotImage.enabled = !_dotImage.enabled;
        }

        if (_scaleTween != null)
        {
            _scaleTween.Kill();
            _scaleTween = null;
            ResetBeforeNextTween();
        }

        _scaleTween = DoScaleTween();
    }

    private Tween DoScaleTween()
    {
        return DOTween.Sequence()
            .Append(transform.DOScale(_scaleSize, 0.1f).SetEase(Ease.InOutSine))
            .Append(transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutElastic))
            .AppendCallback(() => _outline.enabled = _toggle.isOn);
    }

    private void ResetBeforeNextTween() => transform.localScale = Vector3.one;
}
