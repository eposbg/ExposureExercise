using ExposureExercise.Domain;

namespace ExposureExercise.Application
{
    public class ExposureBalancer
    {
        public virtual List<Entity> Rebalance(List<Entity> entities)
        {
            if (!IsValid(entities))
            {
                throw new ArgumentException("The input is not valid");
            }

            var rebalancedEntities = new List<Entity>();

            decimal initalTotalExposure = GetTotalExposure(entities);
            decimal exposureToAllocate = initalTotalExposure;
            var entitisByLowestPriority = entities.OrderByDescending(x => x.Priority);

            foreach (var entity in entitisByLowestPriority)
            {
                var newExposure = Math.Min(exposureToAllocate, entity.Capacity);
                rebalancedEntities.Add(
                    new Entity(
                        entity.EntityId,
                        newExposure,
                        entity.Capacity,
                        entity.Priority));

                exposureToAllocate -= newExposure;
            }

            if (!IsValid(rebalancedEntities))
            {
                throw new Exception("The rebalanced entities are not valid");
            }

            if (GetTotalExposure(rebalancedEntities) != initalTotalExposure)
            {
                throw new Exception("The initial total exposure is not the same as the one after the rebalance");
            }

            return rebalancedEntities;


        }
        public decimal GetTotalExposure(List<Entity> entities)
        {
            return entities.Sum(e => e.Exposure);
        }
        public virtual bool IsValid(List<Entity> entities)
        {
            return entities.All(e => e.Exposure <= e.Capacity);
        }
    }
}
