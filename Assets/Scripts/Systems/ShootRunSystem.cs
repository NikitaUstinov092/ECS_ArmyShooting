using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Threading.Tasks;
using UnityEngine;

namespace Client {
    struct ShootRunSystem : IEcsRunSystem {

        private const float distanceShoot = 50;

        private readonly EcsFilterInject<Inc<ShootComponent, ArmyComponent, UnitTypeComponent>> ecsFilterS;
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (var entityIndex in ecsFilterS.Value)
            {
                ref var shootComponent = ref ecsFilterS.Pools.Inc1.Get(entityIndex);
                ref var armyTypeComponent = ref ecsFilterS.Pools.Inc2.Get(entityIndex);
                ref var unitTypeComponent = ref ecsFilterS.Pools.Inc3.Get(entityIndex);

                if (CheckShootDistance(ref armyTypeComponent, ref unitTypeComponent))
                    StartShooting(ref shootComponent, 5);
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
                    if (Vector3.Distance(currentFighterUnit.transform.position, unitTypeComponent.View.transform.position) < distanceShoot)
                        return true;
                }       
            }
            return false;
        }
        private void StartShooting(ref ShootComponent shootComp, float time)
        {
            GameObject newFighter = Object.Instantiate(shootComp.Bullet, shootComp.Spawn.position, shootComp.Bullet.transform.rotation);
            Rigidbody rb = newFighter.GetComponent<Rigidbody>();
            rb.AddForce(-shootComp.Spawn.forward * 300);
        }
      
    }
}