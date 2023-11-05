
using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public struct BulletEntityFactorySystem: IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<ShootComponent, TeamComponent>> _filterUnit;
    private readonly EcsPoolInject<ShootCountDownComponent> _poolShootCountDown;
    private readonly EcsPoolInject<ViewComponent> _poolView;
    private readonly EcsPoolInject<MoveComponent> _poolMove;
    private readonly EcsPoolInject<HealthComponent> _poolHealth;
    private readonly EcsPoolInject<TeamComponent> _poolTeam;
    private readonly EcsPoolInject<DamageComponent> _poolDamage;
    private readonly EcsPoolInject<BulletSpawnComponent> _poolBullet;
    private readonly EcsPoolInject<BulletDestroyDelayComponent> _poolBulletDestroyDelay;
    
    public void Run(IEcsSystems systems)
    {
        foreach (var unitEntity in _filterUnit.Value)
        {
            ref var shoot = ref _filterUnit.Pools.Inc1.Get(unitEntity);
            
            if (_poolShootCountDown.Value.Has(unitEntity) || !shoot.InFieldOfView) 
                continue;

            _poolShootCountDown.Value.Add(unitEntity).CoolDawnDelay = Random.Range(1, 3); //Попробовать убрать в отдельную систему
            
            ref var team = ref _filterUnit.Pools.Inc2.Get(unitEntity);
            
            var world = systems.GetWorld();
            
            var bulletEntity = world.NewEntity();

            _poolView.Value.Add(bulletEntity);
            _poolMove.Value.Add(bulletEntity);
            _poolHealth.Value.Add(bulletEntity);
            _poolTeam.Value.Add(bulletEntity).TeamType = team.TeamType; 
            _poolBullet.Value.Add(bulletEntity).SpawnTransform = shoot.BulletSpawnPoint;
            _poolDamage.Value.Add(bulletEntity);
            _poolBulletDestroyDelay.Value.Add(bulletEntity);
        }
    }
}
