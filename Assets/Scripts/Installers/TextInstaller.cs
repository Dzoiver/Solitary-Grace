using UnityEngine;
using Zenject;

public class TextInstaller : MonoInstaller
{
    [SerializeField] TextShow text;
    public override void InstallBindings()
    {
        Container.Bind<TextShow>().FromInstance(text).AsSingle();
    }
}