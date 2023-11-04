using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

struct MoveSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<ViewComponent, MoveComponent>> _filterBlock;
    private readonly EcsPoolInject<ViewComponent> _poolView;
    private readonly EcsPoolInject<MoveComponent> _poolMove;
    private const float MoveMultiple = 1000f;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filterBlock.Value)
        {
            ref ViewComponent bv = ref _poolView.Value.Get(entity);
            ref MoveComponent move = ref _poolMove.Value.Get(entity);
            
            if(move.Direction == null || bv.View == null)
                return;

            var position = bv.View.transform.position;
            position =
                Vector3.MoveTowards(position, new Vector3(position.x, position.y, move.Direction.z * MoveMultiple), 
                    move.Speed * Time.deltaTime);
            bv.View.transform.position = position;
        }
    }
}
