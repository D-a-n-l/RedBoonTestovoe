using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Text _name;

    [SerializeField]
    private Text _cost;

    [SerializeField]
    private Image _icon;

    private RectTransform _rectTransform;

    private CanvasGroup _canvasGroup;

    public Inventory _currentInventory;

    private Gold _gold;

    private Vector2 _lastPosition;

    private bool _isPurchased = false;

    public Item Item { get; set; }

    public Vector2 StartPosition { get; set; }

    [Inject]
    public void Construct(Gold gold)
    {
        _gold = gold;
    }

    private void OnEnable()
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(Item item, Inventory inventory)
    {
        Item = item;

        _name.text = Item.Name;

        _cost.text = Item.Cost.ToString();

        _icon.sprite = Item.Icon;

        _currentInventory = inventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;

        _lastPosition = _rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, eventData.position.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Inventory inventory))
        {
            Trade(inventory);
        }
        else
        {
            _rectTransform.anchoredPosition = _lastPosition;
        }

        _canvasGroup.blocksRaycasts = true;
    }

    private void Trade(Inventory inventory)
    {
        if (inventory == _currentInventory)
        {
            _rectTransform.anchoredPosition = _lastPosition;
        }
        else
        {
            if (IsCheckGold(inventory.Type) == true)
            {
                _gold.Increase(DecreaseGold(inventory.Type));
            }
            else
            {
                if (_gold.Current < Item.Cost)
                {
                    _rectTransform.anchoredPosition = _lastPosition;

                    return;
                }

                _gold.Decrease(DecreaseGold(inventory.Type));
            }

            _isPurchased = IsCheckGold(inventory.Type);

            _currentInventory.TransferTo(this, inventory);
        }
    }

    private bool IsCheckGold(TypeInventory typeInventory)
    {
        return typeInventory switch
        {
            TypeInventory.Player => false,
            TypeInventory.Trader => true,
            _ => false
        };
    }

    private int DecreaseGold(TypeInventory typeInventory)
    {
        return typeInventory switch
        {
            TypeInventory.Player => Item.Cost,
            TypeInventory.Trader => Item.Cost / 2,
            _ => 0
        };
    }
}