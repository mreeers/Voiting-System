using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VoitingSystem.Tests
{
    public class MathOne
    {
        private readonly ITestOne testOne;

        public MathOne(ITestOne testOne)
        {
            this.testOne = testOne;
        }

        public int Add(int a, int b) => testOne.Add(a, b);
        public void Out(string msg) => testOne.Out(msg);
    }

    public class MathTestOne
    {
        [Fact]
        public void mathOneAddsTwoNumbers()
        {
            var testOneMock = new Mock<ITestOne>();
            testOneMock.Setup(x => x.Add(1, 1)).Returns(2);
            var mathOne = new MathOne(testOneMock.Object);

            Assert.Equal(2, mathOne.Add(1, 1));
        }

        [Fact]
        public void VerifyFunctionHasBeenCalled()
        {
            var testOneMock = new Mock<ITestOne>();
            var msg = "Hello";
            var mathOne = new MathOne(testOneMock.Object);
            mathOne.Out(msg);
            testOneMock.Verify(x => x.Out(msg), Times.Once);
        }
    }

    public interface ITestOne
    {
        public int Add(int a, int b);
        public void Out(string msg);
    }

    public class TestOne : ITestOne
    {
        public int Add(int a, int b) => a + b;
        public void Out(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public class TestOneTests
    {                
        [Fact]
        public void Add_AddsTwoNumbersTogether()
        {
            var result = new TestOne().Add(1, 1);
            Assert.Equal(2, result);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        public void Add_AddsTwoNumbersTogether_Theory(int a, int b, int expercted)
        {
            var result = new TestOne().Add(a, b);
            Assert.Equal(expercted, result);
        }

        [Fact]
        public void TestListContainsValue()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            Assert.Contains(1, list);
        }
    }
}
