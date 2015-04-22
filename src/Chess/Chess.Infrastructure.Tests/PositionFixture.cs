using Xunit;

namespace Chess.Infrastructure.Tests
{
    public class PositionFixture
    {
        private Position _sut;

        [Fact]
        public void Equals_Should_ReturnTrueForOperatorAndOverride()
        {
            // arrange 
            _sut = new Position(4, 5);
            var test = new Position(new Position(4, 5));

            // act
            var equalsOverride = _sut.Equals(test);
            var equalsOperator = _sut == test;

            // assert
            Assert.True(equalsOverride);
            Assert.True(equalsOperator);
        }

        [Fact]
        public void Equals_Should_ReturnFalseForOperatorAndOverride()
        {
            // arrange 
            _sut = new Position(4, 5);
            var test = new Position(2, 3);

            // act
            var equalsOverride = _sut.Equals(test);
            var equalsOperator = _sut == test;

            // assert
            Assert.False(equalsOverride);
            Assert.False(equalsOperator);
        }

        public void NotEquals_Should_ReturnFalseForOperator()
        {
            // arrange 
            _sut = new Position(4, 5);
            var test = new Position(new Position(4, 5));

            // act
            var equalsOperator = _sut != test;

            // assert
            Assert.False(equalsOperator);
        }

        [Fact]
        public void NotEquals_Should_ReturnTrueForOperatorAndOverride()
        {
            // arrange 
            _sut = new Position(4, 5);
            var test = new Position(2, 3);

            // act
            var equalsOperator = _sut != test;

            // assert
            Assert.True(equalsOperator);
        }

        [Fact]
        public void TestEqualsOperatorNull()
        {
            _sut = new Position();

            // assert
            Assert.True(_sut != null);
            Assert.False(_sut == null);
            Assert.True(null != _sut);
            Assert.False(null == _sut);
        }
    }
}
