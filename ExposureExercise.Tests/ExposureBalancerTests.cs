using ExposureExercise.Application;
using ExposureExercise.Domain;

namespace ExposureExercise.Tests
{
    public class ExposureBalancerTests
    {
        [Fact]
        public void Rebalance_ThrowsExceptionIfInputValuesAreNotValid()
        {
            var entries = new List<Entity>() {
                new Entity("A", 51, 50, 1),
            };

            var sut = new ExposureBalancer();
            Assert.Throws<ArgumentException>(() => sut.Rebalance(entries));
        }

        [Fact]
        public void Rebalance_Success()
        {
            var entries = new List<Entity>() {
                new Entity("A", 40, 50, 1),
                new Entity("B", 30, 60, 2),
                new Entity("C", 20, 40, 3),
                new Entity("D", 10, 20, 4),
            };


            var sut = new ExposureBalancer();
            var result = sut.Rebalance(entries);

            Assert.Equal(result?.Count, 4);
            Assert.Equal(result?.Single(x => x.EntityId == "D").Exposure, 20);
            Assert.Equal(result?.Single(x => x.EntityId == "C").Exposure, 40);
            Assert.Equal(result?.Single(x => x.EntityId == "B").Exposure, 40);
            Assert.Equal(result?.Single(x => x.EntityId == "A").Exposure, 0);

        }
    }
}