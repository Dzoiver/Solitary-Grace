using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] MouseLook mouseLook;
    public override void InstallBindings()
    {
        Container.Bind<MouseLook>().FromInstance(mouseLook).AsSingle();
    }
}