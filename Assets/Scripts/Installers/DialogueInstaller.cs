using UnityEngine;
using Zenject;

public class DialogueInstaller : MonoInstaller
{
    [SerializeField] DialogueManager dManager;
    public override void InstallBindings()
    {
        Container.Bind<DialogueManager>().FromInstance(dManager).AsSingle();
    }
}