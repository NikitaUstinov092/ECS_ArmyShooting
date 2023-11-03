using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

struct MoveSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<ViewComponent>> _filterBlock;
    private readonly EcsPoolInject<ViewComponent> _poolView;
    private readonly EcsPoolInject<MoveComponent> _poolMove;

    public void Run(IEcsSystems systems)
    {
        foreach (int entity in _filterBlock.Value)
        {

            ref ViewComponent bv = ref _poolView.Value.Get(entity);
            
            ref MoveComponent move = ref _poolMove.Value.Get(entity);

            bv.View.transform.position =
                Vector3.MoveTowards(bv.View.transform.position, move.Direction, Time.deltaTime);

        }
    }
}
