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

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        public void Rank_Should_ReturnCorrespondingYStartingFromOne(int y, int expectedRank)
        {
            // arrange
            _sut = new Position(0, y);

            // act
            var actualRank = _sut.Rank;

            // assert
            Assert.Equal(expectedRank, actualRank);
        }

        [Theory]
        [InlineData(0, 'a')]
        [InlineData(1, 'b')]
        [InlineData(2, 'c')]
        [InlineData(3, 'd')]
        [InlineData(4, 'e')]
        [InlineData(5, 'f')]
        [InlineData(6, 'g')]
        [InlineData(7, 'h')]
        public void File_Should_ReturnCorrespondingYStartingFromOne(int x, char expectedFile)
        {
            // arrange
            _sut = new Position(x, 0);

            // act
            var actualFile = _sut.File;

            // assert
            Assert.Equal(expectedFile, actualFile);
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
