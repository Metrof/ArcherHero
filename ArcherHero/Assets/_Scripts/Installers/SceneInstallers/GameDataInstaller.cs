using UnityEngine;
using Zenject;

public class GameDataInstaller : MonoInstaller
{
    [SerializeField] private DataHolderTestZ _data;
    public override void InstallBindings()
    {
        Container.Bind<DataHolderTestZ>().FromInstance(_data).AsSingle().NonLazy();
        //Container.QueueForInject(_data);
    }
}