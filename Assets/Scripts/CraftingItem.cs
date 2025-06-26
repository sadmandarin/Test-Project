using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItem : MonoBehaviour
{
    [SerializeField] private RectTransform _movableContainer;
    [SerializeField] private Image _bg;

    [SerializeField] private Sprite _deselectedSprite;
    [SerializeField] private Sprite _selectedSprite;

    private int _moveDistance;
    private float _movePercentValue = 0.054f;
    private Toggle _toggle;
    private Tween _movingTween;

    private void OnEnable()
    {
        _moveDistance = Mathf.FloorToInt(560 * _movePercentValue);
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ToggleValueChanged);
    }

    private void ToggleValueChanged(bool value)
    {
        if (value)
            CreateMovingTween(new Vector2(_moveDistance, 0), value);
        else
            CreateMovingTween(Vector2.zero, value);
    }

    private void CreateMovingTween(Vector2 movingTarget, bool toggleActive)
    {
        if (_movingTween != null)
        {
            _movingTween.Kill();
            _movingTween = null;
        }

        _movingTween = DOTween.Sequence()
            .Append(_movableContainer.DOAnchorPos(movingTarget, 0.15f)).SetEase(Ease.InOutCubic)
            .AppendCallback(() => _bg.sprite = toggleActive ? _selectedSprite : _deselectedSprite);
    }
}
