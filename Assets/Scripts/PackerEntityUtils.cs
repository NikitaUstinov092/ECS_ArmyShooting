using Leopotam.EcsLite;
using System;
    internal struct PackerEntityUtils
    {
        public static int UnpackEntities(EcsWorld world, EcsPackedEntity firstPacked)
        {
            if (firstPacked.Unpack(world, out int entity1))
                return entity1;
            
            throw new InvalidOperationException("Не удалось распаковать!");
        }
    }
