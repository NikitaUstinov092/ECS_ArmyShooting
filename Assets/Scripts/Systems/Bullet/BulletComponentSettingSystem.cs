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
                
                ref var healthComp = ref _filterBullet.Pools.Inc1.Get(entity);
                ref var teamComp = ref _filterBullet.Pools.Inc2.Get(entity);
                ref var damageComp = ref _filterBullet.Pools.Inc3.Get(entity);
                ref var bulletComp = ref _filterBullet.Pools.Inc4.Get(entity);
                ref var moveComp = ref _filterBullet.Pools.Inc5.Get(entity);
                ref var destroyDelayComp = ref _filterBullet.Pools.Inc6.Get(entity);

                healthComp.Health = data.Health;
                damageComp.Damage = data.Damage;
                destroyDelayComp.Delay = data.DestroyDelay;
               
                SetBulletSpawnView(ref bulletComp, ref teamComp, ref data);
                SetMove(ref moveComp, ref teamComp, ref data);
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
        
        private void SetMove(ref MoveComponent move, ref TeamComponent team,  ref BulletData data)
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