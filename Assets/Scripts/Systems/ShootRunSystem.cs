using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

    struct ShootRunSystem : IEcsRunSystem, IEcsInitSystem
    {     
     private readonly EcsFilterInject<Inc<ShootComponent, ArmyComponent, UnitTypeComponent>> _ecsFilterS;
     private readonly EcsFilterInject<Inc<ShootCountDownComponent>> _shootCountDownPool;
     private readonly EcsCustomInject<SharedData> _data;
     private EcsWorldInject _world;

     private GameObject _bulletParent;

    public void Init(IEcsSystems systems)
    {
        _bulletParent = new GameObject("Bullets");
    }
    public void Run(IEcsSystems systems)
        {
            foreach (var entityIndex in _ecsFilterS.Value)
            {
                ref var shootComponent = ref _ecsFilterS.Pools.Inc1.Get(entityIndex);
                ref var armyTypeComponent = ref _ecsFilterS.Pools.Inc2.Get(entityIndex);
                ref var unitTypeComponent = ref _ecsFilterS.Pools.Inc3.Get(entityIndex);

                if (!CheckShootDistance(ref armyTypeComponent, ref unitTypeComponent)) 
                    continue;
                if (_shootCountDownPool.Pools.Inc1.Has(entityIndex))
                    continue;

                BulletInit();
                
                var bullet = StartShootingAsync(shootComponent);
                bullet.Init(_world.Value);
                
                AddShootCountDownComponent(entityIndex);
            }
        }

    private void BulletInit()
    {
        var world = _world.Value;
        var damagePool = world.GetPool<DamageComponent>();
        var poolHealth = world.GetPool<HealthComponent>();
        var poolArmy = world.GetPool<ArmyComponent>();
        
        var entityBullet = _world.Value.NewEntity();
        
        damagePool.Add(entityBullet);
        poolHealth.Add(entityBullet).Health = 1;;
        poolArmy.Add(entityBullet);
    }

        private bool CheckShootDistance(ref ArmyComponent currentFighter, ref UnitTypeComponent currentUnit)
        {
            var currentFighterTeam = currentFighter.TeamNumber;
            var currentFighterUnit = currentUnit.View;

            foreach (var entityIndex in _ecsFilterS.Value)
            {
                ref var armyTypeComponent = ref _ecsFilterS.Pools.Inc2.Get(entityIndex);
                ref var unitTypeComponent = ref _ecsFilterS.Pools.Inc3.Get(entityIndex);

                if (armyTypeComponent.TeamNumber == currentFighterTeam) 
                    continue;
                if ((currentFighterUnit.transform.position - unitTypeComponent.View.transform.position).sqrMagnitude < _data.Value.ShotDistance)
                    return true;
            }
            return false;
        }

        private ECSMonoObject StartShootingAsync(ShootComponent shootComp)
        {
            var newBullet = Object.Instantiate(shootComp.Bullet, shootComp.Spawn.position, shootComp.Bullet.transform.rotation);
            var rb = newBullet.GetComponent<Rigidbody>();
            rb.AddForce(-shootComp.Spawn.forward * 500);
            newBullet.transform.parent = _bulletParent.transform;
            return newBullet;
        }

        private void AddShootCountDownComponent(int entityIndex)
        {
            _shootCountDownPool.Pools.Inc1.Add(entityIndex);
        }
        
}

