using Leopotam.EcsLite;
using System;
    internal struct PackerEntityUtils
    {
            public static (int, int) UnpackEntities(EcsWorld world, EcsPackedEntity firstPacked, EcsPackedEntity secondPacked)
            {
                if (firstPacked.Unpack(world, out int entity1) &&
                    secondPacked.Unpack(world, out int entity2))
                    return (entity1, entity2);
                else throw new InvalidOperationException("Не удалось распаковать!");
            }

            public static int UnpackEntities(EcsWorld world, EcsPackedEntity firstPacked)
            {
                if (firstPacked.Unpack(world, out int entity1))
                    return entity1;
                else throw new InvalidOperationException("Не удалось распаковать!");
            }
    }
    
