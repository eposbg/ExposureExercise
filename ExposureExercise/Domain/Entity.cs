namespace ExposureExercise.Domain
{
    public class Entity
    {
        public string EntityId { get; }
        public decimal Exposure { get; }
        public decimal Capacity { get; }
        public int Priority { get; }

        public Entity(string entityId, decimal exposure, decimal capacity, int priority)
        {
            EntityId = entityId;
            Exposure = exposure;
            Capacity = capacity;    
            Priority = priority;
        }
    }
}
