using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _textBG;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _deselectedSprite;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _deselectedColor;

    private Tween _chooseTween;
    private Button _btn;
    private InventoryManager _inventoryManager;
    private InventoryItemData _itemData;
    private bool _isSelected;

    public void Initialize(InventoryItemData item, InventoryManager manager)
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(Click);

        _itemData = item;
        _inventoryManager = manager;
        _icon.sprite = item.Icon;
        _amountText.text = item.Amount.ToString();
        _textBG.sprite = _deselectedSprite;
        _amountText.color = _deselectedColor;
        _itemContainer.SetActive(true);

        _isSelected = false;
    }

    private void OnDestroy()
    {
        if (_btn != null)
            _btn.onClick.RemoveListener(Click);
    }

    private void Click()
    {
        if (!_isSelected && _inventoryManager.SelectedItems.Any(x => !x.IsFillen))
        {
            _inventoryManager.SelectItems(_itemData);
            _isSelected = true;
            _amountText.color = _selectedColor;
            _textBG.sprite = _selectedSprite;
        }
    }

    private void ClickTween(Vector3 toVector)
    {
        if (_chooseTween != null)
        {
            _chooseTween.Kill();
            _chooseTween = null;
        }

        _chooseTween = transform.DOScale(toVector, 0.1f).SetEase(Ease.InOutSine);
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
