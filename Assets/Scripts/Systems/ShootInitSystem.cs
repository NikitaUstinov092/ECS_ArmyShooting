using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

struct ShootInitSystem : IEcsInitSystem
{
    private readonly EcsCustomInject<SharedData> _data;
    private readonly EcsFilterInject<Inc<ArmyComponent, UnitTypeComponent>> ecsFilterArmyUnitType;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        EcsPool<ShootComponent> poolShoot = world.GetPool<ShootComponent>();

        for (int i = 0; i < ecsFilterArmyUnitType.Value.GetEntitiesCount(); i++)
        {        
            int entity = world.NewEntity();

            ref var armyComponent = ref ecsFilterArmyUnitType.Pools.Inc1.Get(i);
            ref var unitTypeComponent = ref ecsFilterArmyUnitType.Pools.Inc2.Get(i);

            if(armyComponent.TeamNumber == 0)   
                SetShootCompEntity(entity, poolShoot, unitTypeComponent, "Red Team");

            else if (armyComponent.TeamNumber == 1)
                SetShootCompEntity(entity, poolShoot, unitTypeComponent, "Blue Team");
        }
    }

    private void SetShootCompEntity(int entity, EcsPool<ShootComponent> poolShoot, UnitTypeComponent unit, string targetTag)
    {
        poolShoot.Add(entity).Spawn = unit.View.transform;
        poolShoot.Get(entity).TargetTag = targetTag;
        var bullet = poolShoot.Get(entity).Bullet = _data.Value.Bullet;

    }


}
