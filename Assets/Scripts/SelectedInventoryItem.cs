using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInventoryItem : MonoBehaviour
{
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amount;

    private bool _isFillen = false;

    public bool IsFillen => _isFillen;

    public void Initialize(InventoryItemData itemData)
    {
        _icon.sprite = itemData.Icon;
        _amount.text = itemData.Amount.ToString();
        _isFillen = true;
        ActivateItem();
    }

    private void ActivateItem()
    {
        _itemContainer.transform.localScale = Vector3.one * 0.1f;

        DOTween.Sequence()
            .AppendCallback(() => _itemContainer.SetActive(true))
            .Append(_itemContainer.transform.DOScale(Vector3.one, 0.2f)).SetEase(Ease.OutBounce);
    }
}
