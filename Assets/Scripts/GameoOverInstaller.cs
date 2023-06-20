using UnityEngine;
using Zenject;

public class GameoOverInstaller : MonoInstaller
{
    [SerializeField] GameOver gOver;
    public override void InstallBindings()
    {
        Container.Bind<GameOver>().FromInstance(gOver).AsSingle();
    }
}