using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMap;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Dtos;

namespace Tests
{
    [TestClass]
    public class BasicTests
    {
        IMapper Mapper { get; set; }
        Person Person { get; set; }

        private void Arrange()
        {
            Mapper = new Mapper();
            var user = new User { Id = 15, IsActive = true, Username = "test" };
            Person = new Person
            {
                AccountBalance = 21.23m,
                DateAdded = DateTime.Now,
                FavoriteNumber = 55.5,
                Firstname = "Sam",
                Lastname = "North",
                GlobalDateAdded = DateTimeOffset.Now,
                Id = 15,
                IsActive = true,
                NullableInt = 99,
                ReferenceType = user
            };
        }

        [TestMethod]
        public void IntMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(15, personDto.Id);
        }

        [TestMethod]
        public void DecimalMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(21.23m, personDto.AccountBalance);
        }

        [TestMethod]
        public void DoubleMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);
            
            Assert.AreEqual(55.5, personDto.FavoriteNumber);
        }

        [TestMethod]
        public void DateTimeMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(Person.DateAdded, personDto.DateAdded);
        }

        [TestMethod]
        public void DateTimeOffsetMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(Person.GlobalDateAdded, personDto.GlobalDateAdded);
        }

        [TestMethod]
        public void BoolMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(true, personDto.IsActive);
        }

        [TestMethod]
        public void StringMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual("Sam", personDto.Firstname);
            Assert.AreEqual("North", personDto.Lastname);
        }

        [TestMethod]
        public void ReferenceTypeMapSucceedsAsClone()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Person.ReferenceType.Username = "ham";
            Assert.AreNotEqual(Person.ReferenceType.Username, personDto.ReferenceType.Username);
        }

        [TestMethod]
        public void NullableIntMapSucceeds()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(personDto.NullableInt, personDto.NullableInt);
        }

        [TestMethod]
        public void NoSetterMapSkips()
        {
            Arrange();

            var personDto = Mapper.Map<PersonDto>(Person);

            Assert.AreEqual(null, personDto.Fullname);
        }

        [TestMethod]
        public void TypeMismatchExceptionThrown()
        {
            Arrange();
            try
            {
                var personDto = Mapper.Map<PersonDto>(Person);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is PropertyTypeMismatchException);
            }
        }

        [TestMethod]
        public void SourceArgumentExceptionsThrown()
        {
            Arrange();
            try
            {
                var personDto = Mapper.Map<PersonDto>(null);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }
        }

        [TestMethod]
        public void SourceToSourceArgumentExceptionsThrown()
        {
            Person person = null;
            try
            {
                var mappedPerson = Mapper.Map(person);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is NullReferenceException);
            }
        }

        [TestMethod]
        public void DestinationArgumentExceptionsThrown()
        {
            Arrange();
            try
            {
                PersonDto personDto = null;
                var mapped = Mapper.Map(Person, personDto);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is ArgumentNullException);
            }
        }
    }
}
