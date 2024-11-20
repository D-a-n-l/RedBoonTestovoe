using System;
using UnityEngine;

public class Gold : Stat
{
    [Min(0)]
    [SerializeField]
    private int _baseGold = 0;

    public override void Init()
    {
        Increase(_baseGold);
    }

    public override void Decrease(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "value must be positive.");

        Current -= value;

        OnDecreased.Invoke();

        if (Current <= 0)
        {
            Current = 0;

            OnZeroing.Invoke();
        }
    }

    public override void Increase(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "value must be positive.");

        Current += value;

        OnIncreased.Invoke();
    }
}