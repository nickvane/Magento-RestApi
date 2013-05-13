using Magento.RestApi.Json;
using Moq;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Magento.RestApi.UnitTests.Json
{
    [TestFixture]
    public class BoolConverterTests
    {
        [Test]
        public void CanConvert()
        {
            // arrange
            var converter = new BoolConverter();
            
            // act
            var result1 = converter.CanConvert(false.GetType());
            var result2 = converter.CanConvert(typeof(bool?));
            var result3 = converter.CanConvert("".GetType());

            // assert
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsFalse(result3);
        }

        [Test]
        public void WritesCorrectly()
        {
            // arrange
            var converter = new BoolConverter();
            var writer1 = new Moq.Mock<JsonWriter>();
            writer1.Setup(x => x.WriteValue("0")).Verifiable();
            var writer2 = new Moq.Mock<JsonWriter>();
            writer2.Setup(x => x.WriteValue("1")).Verifiable();
            var writer3 = new Moq.Mock<JsonWriter>();

            // act
            converter.WriteJson(writer1.Object, false, null);
            converter.WriteJson(writer2.Object, true, null);
            converter.WriteJson(writer3.Object, null, null);

            // assert
            writer1.Verify();
            writer2.Verify();
            writer3.Verify(x => x.WriteValue(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void ReadsCorrectly()
        {
            // arrange
            var converter = new BoolConverter();
            var reader1 = new Moq.Mock<JsonReader>();
            reader1.SetupGet(x => x.Value).Returns(null);
            var reader2 = new Moq.Mock<JsonReader>();
            reader2.SetupGet(x => x.Value).Returns("0");
            var reader3 = new Moq.Mock<JsonReader>();
            reader3.SetupGet(x => x.Value).Returns("1");

            // act
            var object1 = converter.ReadJson(reader1.Object, typeof(bool), null, null);
            var object2 = converter.ReadJson(reader1.Object, typeof(bool?), null, null);
            var object3 = converter.ReadJson(reader2.Object, typeof(bool), null, null);
            var object4 = converter.ReadJson(reader3.Object, typeof(bool), null, null);

            // assert
            Assert.IsAssignableFrom<bool>(object1);
            Assert.IsFalse((bool)object1);
            Assert.IsNull(object2);
            Assert.IsFalse((bool)object3);
            Assert.IsTrue((bool)object4);
        }
    }
}
