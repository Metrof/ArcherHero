﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IBehaviorCreateProjectile
{
    List<Projectile> Create(Transform pointSpawnProjectile, Transform target, ObjectPool<Projectile> pool);
}