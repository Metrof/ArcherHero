using UnityEngine;
using Zenject;

public class DataDontDestroyOnLoadInstaller : MonoInstaller
{
    [SerializeField] private DataHolderTestZ _projectData;
    public override void InstallBindings()
    {
        DataHolderTestZ data = Instantiate(_projectData);
        Container.Bind<DataHolderTestZ>().FromInstance(data).NonLazy();
    }
}