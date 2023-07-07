using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public struct ECSInitialazer : IEcsInitSystem
    {
        private readonly EcsCustomInject<SharedData> _data;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            EcsPool<UnitTypeComponent> poolBlock = world.GetPool<UnitTypeComponent>();
            EcsPool<HealthComponent> poolHealth = world.GetPool<HealthComponent>();
            EcsPool<DamageComponent> poolDamage = world.GetPool<DamageComponent>();
            EcsPool<ArmyComponent> poolArmy = world.GetPool<ArmyComponent>();
            EcsPool<MoveComponent> poolMove = world.GetPool<MoveComponent>();

            int armyCount = 2; // ����� ���������� �����
            int entitiesPerArmy = _data.Value.CountSpawnInRow * _data.Value.Row; // ���������� ��������� �� ������ �����

            for (int i = 0; i < entitiesPerArmy * armyCount; i++)
            {
                int entity = world.NewEntity();
                poolBlock.Add(entity);
                poolMove.Add(entity);
                poolHealth.Add(entity).Health = 100;
                poolDamage.Add(entity).Damage = 100;

                // ����������� ������ �������� �����
                int armyId = i / entitiesPerArmy; // ����� ������ �� ���������� ��������� �� ������ �����
                poolArmy.Add(entity).TeamNumber = armyId;
            }
        }
    }
}