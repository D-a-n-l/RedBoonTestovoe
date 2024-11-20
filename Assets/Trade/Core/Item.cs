using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; } = "Name";

    [field: Min(1)]
    [field: SerializeField]
    public int Cost { get; private set; } = 1;

    [field: SerializeField]
    public Sprite Icon { get; private set; }
}