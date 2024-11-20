using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Transform _root;

    [SerializeField]
    private ItemSlot _prefabItemSlot;

    [field: SerializeField]
    public TypeInventory Type {  get; private set; }

    private List<ItemSlot> _items = new List<ItemSlot>();

    public void AddNewItem(Item item)
    {
        ItemSlot itemSlot = Instantiate(_prefabItemSlot, _root);

        itemSlot.Init(item, this);

        itemSlot.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        _items.Add(itemSlot);
    }

    public void TransferTo(ItemSlot itemSlot, Inventory newInventory)
    {
        _items.Remove(itemSlot);

        newInventory.AddItem(itemSlot);
    }

    private void AddItem(ItemSlot itemSlot)
    {
        itemSlot.transform.parent = _root;

        itemSlot.Init(itemSlot.Item, this);

        itemSlot.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        _items.Add(itemSlot);
    }
}