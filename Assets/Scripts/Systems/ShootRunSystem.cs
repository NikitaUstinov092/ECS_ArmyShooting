using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

    struct ShootRunSystem : IEcsRunSystem
    {     
        private readonly EcsFilterInject<Inc<ShootComponent, ArmyComponent, UnitTypeComponent>> ecsFilterS;
        private readonly EcsFilterInject<Inc<ShootCountDownComponent>> shootCountDownPool;
        private EcsWorld _world;
        private const float distanceShoot = 3000;
        public void Run(IEcsSystems systems)
        {
            if (_world == null)
                _world = systems.GetWorld();

            foreach (var entityIndex in ecsFilterS.Value)
            {
                ref var shootComponent = ref ecsFilterS.Pools.Inc1.Get(entityIndex);
                ref var armyTypeComponent = ref ecsFilterS.Pools.Inc2.Get(entityIndex);
                ref var unitTypeComponent = ref ecsFilterS.Pools.Inc3.Get(entityIndex);

                if (CheckShootDistance(ref armyTypeComponent, ref unitTypeComponent))
                {
                    if (!shootCountDownPool.Pools.Inc1.Has(entityIndex))
                    {
                        var bullet = StartShootingAsync(shootComponent);
                        bullet.Init(_world);
                        AddShootCountDownComponent(entityIndex);
                    }
                }

            }
        }

        private bool CheckShootDistance(ref ArmyComponent currentFighter, ref UnitTypeComponent currentUnit)
        {
            var currentFighterTeam = currentFighter.TeamNumber;
            var currentFighterUnit = currentUnit.View;

            foreach (var entityIndex in ecsFilterS.Value)
            {
                ref var armyTypeComponent = ref ecsFilterS.Pools.Inc2.Get(entityIndex);
                ref var unitTypeComponent = ref ecsFilterS.Pools.Inc3.Get(entityIndex);

                if (armyTypeComponent.TeamNumber != currentFighterTeam)
                {
                    if ((currentFighterUnit.transform.position - unitTypeComponent.View.transform.position).sqrMagnitude < distanceShoot)
                        return true;
                }
            }
            return false;
        }

        private ECSMonoObject StartShootingAsync(ShootComponent shootComp)
        {
            ECSMonoObject newFighter = Object.Instantiate(shootComp.Bullet, shootComp.Spawn.position, shootComp.Bullet.transform.rotation);
            Rigidbody rb = newFighter.GetComponent<Rigidbody>();
            rb.AddForce(-shootComp.Spawn.forward * 500);
            return newFighter;
        }

        private void AddShootCountDownComponent(int entityIndex)
        {
            shootCountDownPool.Pools.Inc1.Add(entityIndex);
        }
    }

