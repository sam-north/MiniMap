using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMap;
using System;
using System.Collections.Generic;
using Tests.Dtos;

namespace Tests
{
    [TestClass]
    public class CollectionTest
    {
        IMapper Mapper { get; set; }
        Person Person { get; set; }
        List<Person> People { get; set; } = new List<Person>();
        List<PersonDto> personDtoList = new List<PersonDto>();

        private void Arrange()
        {
            Mapper = new Mapper();
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

            };
            for (int i = 0; i < 200; i++)
                People.Add(Person);

            var personDto = new PersonDto();
            personDto.AccountBalance = 32;
            personDto.ActualNull = null;
            personDto.DateAdded = Convert.ToDateTime("12-15-2018");
            personDto.FavoriteNumber = 444;
            personDto.Firstname = "BLIMB";
            personDto.GlobalDateAdded = Convert.ToDateTime("3-15-1918");
            personDto.Id = 23;
            personDto.IsActive = true;
            personDto.Lastname = "Rabbit";
            personDto.NullableInt = 423;
            for (int i = 0; i < 17; i++)
                personDtoList.Add(personDto);
        }

        [TestMethod]
        public void MapToSame_SameTypeToNewCollection_MapsCorrectCount()
        {
            Arrange();

            var newPeople = Mapper.Map(People);

            Assert.AreEqual(200, newPeople.Count);
        }

        [TestMethod]
        public void MapToSame_WithNullSource_ThrowsException()
        {
            Arrange();
            People = null;

            Assert.ThrowsException<ArgumentNullException>(() => Mapper.Map(People));
        }

        [TestMethod]
        public void MapToDestination_ToNewCollection_MapsCorrectCount()
        {
            Arrange();

            var newPeople = Mapper.Map<List<PersonDto>>(People);

            Assert.AreEqual(200, newPeople.Count);
        }

        [TestMethod]
        public void MapToDestination_WithCollectionSourceAndNonCollectionDestination_ShouldThrowException()
        {
            Arrange();
            Assert.ThrowsException<CollectionMismatchException>(() => Mapper.Map<PersonDto>(People));
        }

        [TestMethod]
        public void MapToDestination_WithNonCollectionSourceAndCollectionDestination_ShouldThrowException()
        {
            Arrange();
            Assert.ThrowsException<CollectionMismatchException>(() => Mapper.Map<List<PersonDto>>(Person));
        }

        [TestMethod]
        public void MapToDestination_WithNullSource_ThrowsException()
        {
            Arrange();
            People = null;

            Assert.ThrowsException<ArgumentNullException>(() => Mapper.Map<List<PersonDto>>(People));
        }

        [TestMethod]
        public void MapToExistingDestination_WithExistingData_MapsCorrectCount()
        {
            Arrange();

            var newPeople = Mapper.Map(People, personDtoList);

            Assert.AreEqual(217, newPeople.Count);
        }

        [TestMethod]
        public void MapToExistingDestination_WithNullSource_ThrowsException()
        {
            Arrange();
            People = null;

            Assert.ThrowsException<ArgumentNullException>(() => Mapper.Map(People, personDtoList));
        }

        [TestMethod]
        public void MapToExistingDestination_WithNullDestination_ThrowsException()
        {
            Arrange();
            personDtoList = null;

            Assert.ThrowsException<ArgumentNullException>(() => Mapper.Map(People, personDtoList));
        }
    }
}
