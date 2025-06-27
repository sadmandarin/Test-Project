using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MajorProjectAnimation : MonoBehaviour
{
    public static MajorProjectAnimation Instance; 

    [Header("Windows Anim Component")]
    [SerializeField] private RectTransform _filterTransform;
    [SerializeField] private RectTransform _manualCraftingTransform;
    [SerializeField] private RectTransform _inventoryTransform;
    [SerializeField] private float _skaleInTime;

    [Header("PC Anim Component")]
    [SerializeField] private Image _resourceImage;
    [SerializeField] private TextMeshProUGUI _resourceName;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private float _fadeInTime;
    private Tween _pcTween;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        WindowsAnimation();
    }

    private void WindowsAnimation()
    {
        DOTween.Sequence()
            .AppendCallback(() => _filterTransform.localScale = Vector3.one * 0.1f)
            .AppendCallback(() => _manualCraftingTransform.localScale = Vector3.one * 0.1f)
            .AppendCallback(() => _inventoryTransform.localScale = Vector3.one * 0.1f)
            .Append(_filterTransform.DOScale(Vector3.one, _skaleInTime)).SetEase(Ease.OutBounce)
            .Append(_manualCraftingTransform.DOScale(Vector3.one, _skaleInTime)).SetEase(Ease.OutBounce)
            .Append(_inventoryTransform.DOScale(Vector3.one, _skaleInTime)).SetEase(Ease.OutBounce);
    }

    public void ShowPCSource(string name, Sprite icon)
    {
        if (_pcTween != null)
        {
            _pcTween.Kill();
            _pcTween = null;
        }

        //Get ready for anim
        _resourceImage.enabled = false;
        _resourceName.enabled = false;
        _description.enabled = false;
        _amount.enabled = false;
        _resourceImage.DOFade(0, 0);
        _resourceName.DOFade(0, 0);
        _description.DOFade(0, 0);
        _amount.DOFade(0, 0);


        _pcTween = DOTween.Sequence()
            .AppendCallback(() => _resourceImage.sprite = icon)
            .AppendCallback(() => _resourceName.text = name)
            .AppendCallback(() => _resourceName.enabled = true)
            .AppendCallback(() => _resourceImage.enabled = true)
            .AppendCallback(() => _description.enabled = true)
            .AppendCallback(() => _amount.enabled = true)
            .Append(_resourceImage.DOFade(1, _fadeInTime))
            .Join(_resourceName.DOFade(1, _fadeInTime))
            .Join(_description.DOFade(1, _fadeInTime))
            .Join(_amount.DOFade(1, _fadeInTime));
    }
}
