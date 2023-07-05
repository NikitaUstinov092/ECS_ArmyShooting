using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    public struct FighterInitialazer : IEcsInitSystem 
    {
        private readonly EcsCustomInject<SharedData> _data;
        public void Init (IEcsSystems systems) 
        {
            var world = systems.GetWorld();
            EcsPool<BlockViewComponent> poolBlock = world.GetPool<BlockViewComponent>();
            EcsPool<HealthComponent> poolHealth = world.GetPool<HealthComponent>();
            EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();

            for (int i = 0; i < _data.Value.CountSpawnInRow * _data.Value.Row; i++)
            {
                int entity = world.NewEntity();
                poolBlock.Add(entity);
                poolHealth.Add(entity).Health = 100;
                poolDamage.Add(entity).Damage = 100;
            }
        }
    }
}