using System.Collections.Generic;
using Magento.RestApi.Core;
using NUnit.Framework;

namespace Magento.RestApi.UnitTests.Core
{
    [TestFixture]
    public class WhenConstructingIntProperty
    {
        [Test]
        public void ForIntShouldHaveDefaults()
        {
            // arrange
            Property<int> property = null;

            // act
            property = new Property<int>();

            // assert
            Assert.AreEqual(0, property.InitialValue);
            Assert.AreEqual(0, property.Value);
            Assert.IsFalse(property.HasChanged());
        }

        [Test]
        public void ForStringShouldHaveDefaults()
        {
            // arrange
            Property<string> property = null;

            // act
            property = new Property<string>();

            // assert
            Assert.IsNullOrEmpty(property.InitialValue);
            Assert.IsNullOrEmpty(property.Value);
            Assert.IsFalse(property.HasChanged());
        }
    }

    [TestFixture]
    public class WhenChangingValueOnIntProperty
    {
        [Test]
        public void HasChangedShouldBeTrue()
        {
            // arrange
            var property = new Property<int>();

            // act
            property.Value = 1;

            // assert
            Assert.IsTrue(property.HasChanged());
            Assert.AreEqual(0, property.InitialValue);
            Assert.AreEqual(1, property.Value);
        }

        [Test]
        public void HasChangedShouldBeFalseAfterInitialValueIsSet()
        {
            // arrange
            var property = new Property<int>();

            // act
            property.Value = 1;
            property.SetValueAsInitial();

            // assert
            Assert.IsFalse(property.HasChanged());
            Assert.AreEqual(1, property.InitialValue);
            Assert.AreEqual(1, property.Value);
        }

        [Test]
        public void HasChangedShouldBeTrueAfterInitialValueIsSetAndChanged()
        {
            // arrange
            var property = new Property<int>();

            // act
            property.Value = 1;
            property.SetValueAsInitial();
            property.Value = 2;

            // assert
            Assert.IsTrue(property.HasChanged());
            Assert.AreEqual(1, property.InitialValue);
            Assert.AreEqual(2, property.Value);
        }
    }

    [TestFixture]
    public class WhenChangingValueOnListProperty
    {
        [Test]
        public void HasChangedShouldBeTrue()
        {
            // arrange
            var property = new Property<List<int>>();

            // act
            property.Value = new List<int>();

            // assert
            Assert.IsTrue(property.HasChanged());
            Assert.IsNull(property.InitialValue);
            Assert.AreEqual(0, property.Value.Count);
        }

        [Test]
        public void HasChangedShouldBeFalseAfterInitialValueIsSet()
        {
            // arrange
            var property = new Property<List<int>>();

            // act
            property.Value = new List<int>();
            property.SetValueAsInitial();

            // assert
            Assert.IsFalse(property.HasChanged());
            Assert.AreEqual(0, property.InitialValue.Count);
            Assert.AreEqual(0, property.Value.Count);
        }

        [Test]
        public void HasChangedShouldBeTrueAfterInitialValueIsSetAndChanged()
        {
            // arrange
            var property = new Property<List<int>>();

            // act
            property.Value = new List<int>();
            property.SetValueAsInitial();
            property.Value = new List<int>{1};

            // assert
            Assert.IsTrue(property.HasChanged());
            Assert.AreEqual(0, property.InitialValue.Count);
            Assert.AreEqual(1, property.Value.Count);
        }

        [Test]
        public void HasChangedShouldBeFalseAfterInitialValueIsSetAndSameItemIsAdded()
        {
            // arrange
            var property = new Property<List<int>>();

            // act
            property.Value = new List<int> { 1 };
            property.SetValueAsInitial();
            property.Value = new List<int> { 1 };

            // assert
            Assert.IsFalse(property.HasChanged());
            Assert.AreEqual(1, property.InitialValue.Count);
            Assert.AreEqual(1, property.Value.Count);
        }

        [Test]
        public void HasChangedShouldBeTrueAfterInitialValueIsSetAndOtherItemIsAdded()
        {
            // arrange
            var property = new Property<List<int>>();

            // act
            property.Value = new List<int> { 1 };
            property.SetValueAsInitial();
            property.Value = new List<int> { 2 };

            // assert
            Assert.IsTrue(property.HasChanged());
            Assert.AreEqual(1, property.InitialValue.Count);
            Assert.AreEqual(1, property.Value.Count);
        }

        [Test]
        public void HasChangedShouldBeTrueAfterInitialValueIsSetAndItemIsChanged()
        {
            // arrange
            var property = new Property<List<int>>();

            // act
            property.Value = new List<int> { 1 };
            property.SetValueAsInitial();
            property.Value[0] = 2;

            // assert
            Assert.IsTrue(property.HasChanged());
            Assert.AreEqual(1, property.InitialValue.Count);
            Assert.AreEqual(1, property.InitialValue[0]);
            Assert.AreEqual(1, property.Value.Count);
            Assert.AreEqual(2, property.Value[0]);
        }
    }

    //[TestFixture]
    //public class WhenConstructingListWithIChangeTrackingProperty
    //{
    //    [Test]
    //    public void ShouldCheckHasChanged()
    //    {
    //        // arrange
    //        var property = new Property<List<ChangeTrackingTest>>();

    //        // act
    //        property = new Property<ChangeTrackingTest>();

    //        // assert
    //        Assert.IsNull(property.InitialValue);
    //        Assert.IsNull(property.Value);
    //        Assert.IsFalse(property.HasChanged());
    //    }
    //}
}
