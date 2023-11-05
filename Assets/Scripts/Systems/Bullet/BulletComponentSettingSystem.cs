using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Views;

namespace Systems.Bullet
{
    public struct BulletComponentSettingSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthComponent, TeamComponent, DamageComponent, BulletSpawnComponent, MoveComponent, BulletDestroyDelayComponent>> _filterBullet;
        private readonly EcsCustomInject<BulletData> _bulletData;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterBullet.Value)
            { 
                var data = _bulletData.Value;
                
                ref var health = ref _filterBullet.Pools.Inc1.Get(entity);
                ref var team = ref _filterBullet.Pools.Inc2.Get(entity);
                ref var damage = ref _filterBullet.Pools.Inc3.Get(entity);
                ref var bullet = ref _filterBullet.Pools.Inc4.Get(entity);
                ref var move = ref _filterBullet.Pools.Inc5.Get(entity);
                ref var destroyDelay = ref _filterBullet.Pools.Inc6.Get(entity);

                health.Health = data.Health;
                damage.Damage = data.Damage;
                destroyDelay.Delay = data.DestroyDelay;
               
                SetBulletSpawnView(ref bullet, ref team, ref data);
                SetMoveComp(ref move, ref team, ref data);
            }
        }
        
        private void SetBulletSpawnView(ref BulletSpawnComponent bulletSpawn, ref TeamComponent team, ref BulletData data)
        {
            bulletSpawn.Bullet = team.TeamType switch
            {
                1 => data.BulletBlue,
                2 => data.BulletRed,
            };
        }
        
        private void SetMoveComp(ref MoveComponent move, ref TeamComponent team,  ref BulletData data)
        {
            move.Speed = data.Speed;
            move.Direction = team.TeamType switch
            {
                1 => -Vector3.forward,
                2 => Vector3.forward,
                _ => move.Direction
            };
        }
    }
}