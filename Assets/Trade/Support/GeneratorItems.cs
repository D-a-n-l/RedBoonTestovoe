using UnityEngine;

public class GeneratorItems : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private int _countItemsSpawn;

    [SerializeField]
    private Item[] _items;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        for (int i = 0; i < _countItemsSpawn; i++)
        {
            int item = Random.Range(0, _items.Length);

            _inventory.AddNewItem(_items[item]);
        }
    }
}