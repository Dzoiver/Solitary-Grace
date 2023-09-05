using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] Menu menu;
    [SerializeField] Inventory inventory;
    public override void InstallBindings()
    {
        Container.Bind<Menu>().FromInstance(menu).AsSingle();
        Container.Bind<Inventory>().FromInstance(inventory).AsSingle();
    }
}