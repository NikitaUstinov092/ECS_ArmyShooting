using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

internal struct DamageSystem : IEcsRunSystem
    {
            private readonly EcsFilterInject<Inc<HitComponent>> _filterHits;
            private readonly EcsPoolInject<HitComponent> _poolHits;
            private readonly EcsPoolInject<DamageComponent> _poolDamage;
            private readonly EcsPoolInject<TeamComponent> _poolTeam;
            private readonly EcsPoolInject<HealthComponent> _poolHealth;
            private readonly EcsWorldInject _world;
            public void Run(IEcsSystems systems)
            {
                foreach(var entity in _filterHits.Value)
                {
                    ref HitComponent hitC = ref _poolHits.Value.Get(entity);
                    
                    (int, int) entitiesCollide =
                        PackerEntityUtils.UnpackEntities(_world.Value, hitC.FirstCollider.EcsPacked, hitC.SecondCollider.EcsPacked);
                    
                    ref TeamComponent firstCollideTeam = ref _poolTeam.Value.Get(entitiesCollide.Item1);
                    ref TeamComponent secondCollideTeam = ref _poolTeam.Value.Get(entitiesCollide.Item2);
                    
                    ref HealthComponent healthC = ref _poolHealth.Value.Get(entitiesCollide.Item1);
                    ref DamageComponent damageC = ref _poolDamage.Value.Get(entitiesCollide.Item2);

                    if (firstCollideTeam.TeamType != secondCollideTeam.TeamType)
                    {
                        healthC.Health -= damageC.Damage;
                    }
                    _world.Value.DelEntity(entity);
                }
            }
    }
