
using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public struct BulletEntityFactorySystem: IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<ShootComponent, TeamComponent>> _filterUnit;
    private readonly EcsPoolInject<ShootCountDownComponent> _poolShootCountDown;
    
    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filterUnit.Value)
        {
            ref var shoot = ref _filterUnit.Pools.Inc1.Get(entity);
            
            if (_poolShootCountDown.Value.Has(entity) || !shoot.InFieldOfView) 
                continue;

            _poolShootCountDown.Value.Add(entity).CoolDawnDelay = Random.Range(1, 3);
            
            ref var team = ref _filterUnit.Pools.Inc2.Get(entity);
            
            var world = systems.GetWorld();
            
            var poolView = world.GetPool<ViewComponent>();
            var poolMove = world.GetPool<MoveComponent>();
            var poolHealth = world.GetPool<HealthComponent>();
            var poolTeam = world.GetPool<TeamComponent>();
            var poolDamage = world.GetPool<DamageComponent>();
            var poolBullet = world.GetPool<BulletComponent>();
            
            var bulletEntity = world.NewEntity();

            poolView.Add(bulletEntity);
            poolMove.Add(bulletEntity);
            poolHealth.Add(bulletEntity);
            poolTeam.Add(bulletEntity).TeamType = team.TeamType; 
            poolBullet.Add(bulletEntity).SpawnTransform = shoot.BulletSpawnPoint;
         
            poolDamage.Add(bulletEntity);
        }
    }
}
