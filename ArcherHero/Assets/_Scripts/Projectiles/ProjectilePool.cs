using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public partial class ProjectilePool : MonoBehaviour
{
    [SerializeField] public List<OwnerPoolData> _ownerPoolData;

    private Dictionary<ProjectileOwner, Dictionary<TypeDamage, ObjectPool<Projectile>>> _poolDictionary = new();
    private AudioManager _audioManager;

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    private void Awake()
    {
        CreateAllPools();
    }

    public ObjectPool<Projectile> GetPool(ProjectileOwner owner, TypeDamage typeDamage)
    {
        return _poolDictionary[owner][typeDamage];
    }

    private void CreateAllPools()
    {
        foreach (var owner in _ownerPoolData)
        {
            GameObject parentGo = CreateGOAndSetParent(transform, owner.ProjectileOwner);

            _poolDictionary.Add(owner.ProjectileOwner, new());

            foreach (var pool in owner.Pools)
            {
                GameObject childrenGo = CreateGOAndSetParent(parentGo.transform, pool.TypeDamage);

                _poolDictionary[owner.ProjectileOwner].Add(pool.TypeDamage, CreatePool(pool, childrenGo.transform, owner.ProjectileOwner));
            }
        }
    }

    private GameObject CreateGOAndSetParent(Transform parentTransform, Enum name)
    {
        GameObject childrenGo = new GameObject($"{name}");
        childrenGo.transform.parent = parentTransform;
        return childrenGo;
    }

    private ObjectPool<Projectile> CreatePool(ProjectilePoolData data, Transform container, ProjectileOwner owner)
    {
        ProjectileSounds sound = new(owner);

        ObjectPool<Projectile> projectilePool = null;
        ObjectPool<Projectile> pool = new(
                    () =>
                    {
                        Projectile projectile = Instantiate(data.Prefab, container);
                        projectile.OnEnabledEvent += () => _audioManager.Play(sound.NumSoundShot);
                        _audioManager.Play(sound.NumSoundShot);
                        projectile.ProjectilePool = projectilePool;
                        return projectile;
                    },
                    projectileGet =>
                    {
                        projectileGet.gameObject.SetActive(true);
                    },
                    projectileRelease =>
                    {
                        projectileRelease.gameObject.SetActive(false);
                    },
                    projectileDestroy =>
                    {
                        Destroy(projectileDestroy.gameObject);
                    },
                    false,
                    data.PoolCount,
                    data.PoolMaxCount
                );

        projectilePool = pool;

        return projectilePool;
    }
}
