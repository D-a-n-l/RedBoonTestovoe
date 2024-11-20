using UnityEngine;
using Zenject;

public class GoldInstaller : MonoInstaller
{
    [SerializeField]
    private Gold _gold;

    public override void InstallBindings()
    {
        Container.Bind<Gold>().FromInstance(_gold);
    }
}