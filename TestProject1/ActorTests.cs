using RESTbottle.Models;
using RESTbottle.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    public class UnitTestActorsRepository
    {

        [Fact]
        public void TestGetMethod()
        {
            //Arrange
            ActorsRepository actorsRepository = new ActorsRepository();
            Actor b = new Actor { } as Actor;

        }

        [Fact]

        public void TestGetActorById_Returns_Correct_Actor()
        {
            //Arrange
            ActorsRepository actorsRepository = new ActorsRepository();
            Actor a1 = new Actor { Name = "Actor 1", BirthYear = new DateTime(1980, 1, 1) };
            Actor a2 = new Actor { Name = "Actor 2", BirthYear = new DateTime(1990, 1, 1) };
            actorsRepository.AddNewActor(a1);
            actorsRepository.AddNewActor(a2);
            //Act
            var actorById = actorsRepository.GetActorById(2);
            //Assert
            Assert.Equal(a2, actorById);

        }

        [Fact]

        public void TestAddActor()
        {
            //Arrange
            ActorsRepository actorsRepository = new ActorsRepository();
            Actor a = new Actor { Name = "Test Actor", BirthYear = new DateTime(1990, 1, 1) };
            //Act
            Actor addedActor = actorsRepository.AddNewActor(a);
            //Assert
            Assert.Equal(1, addedActor.Id);
        }

        [Fact]
        public void TestDeleteActor()
        {
            //Arrange
            ActorsRepository actorsRepository = new ActorsRepository();
            Actor a = new Actor { Name = "Test Actor", BirthYear = new DateTime(1990, 1, 1) };
            Actor addedActor = actorsRepository.AddNewActor(a);
            //Act
            Actor? deletedActor = actorsRepository.DeleteActorById(addedActor.Id);
            //Assert
            Assert.NotNull(deletedActor);
            Assert.Equal(addedActor.Id, deletedActor?.Id);
        }
        [Fact]

        public void TestUpdateActor_Returns_Updated_Actor()
        {
            //Arrange 

            ActorsRepository actorsRepository = new ActorsRepository();
            Actor ActorToUpdate = new Actor { Name = "Old Name", BirthYear = new DateTime(1980, 1, 1) };

            Actor addedActor = actorsRepository.AddNewActor(ActorToUpdate);

            //Act

            Actor updatedActorInfo = new Actor { Name = "New Name", BirthYear = new DateTime(1990, 1, 1) };

            Actor? updatedActor = actorsRepository.UpdateActor(addedActor.Id, updatedActorInfo);

            //Assert
            Assert.NotNull(updatedActor);
            Assert.NotNull(updatedActorInfo);
            Assert.NotNull(updatedActor);

        }

        [Fact]

        public void TestUpdateActor_Returns_Null_For_Nonexistent_Actor()
        {
            //Arrange
            ActorsRepository actorsRepository = new ActorsRepository();
            Actor updatedActorInfo = new Actor { Name = "New Name", BirthYear = new DateTime(1990, 1, 1) };
            //Act
            Actor? updatedActor = actorsRepository.UpdateActor(999, updatedActorInfo); // Non-existent ID
            //Assert
            Assert.Null(updatedActor);

        }

        [Fact]

        public void TestGetMethodWithFilters()
        {
            //Arrange
            ActorsRepository actorsRepository = new ActorsRepository();
            Actor a1 = new Actor { Name = "Actor 1", BirthYear = new DateTime(1980, 1, 1) };
            Actor a2 = new Actor { Name = "Actor 2", BirthYear = new DateTime(1990, 1, 1) };
            Actor a3 = new Actor { Name = "Test Actor", BirthYear = new DateTime(2000, 1, 1) };
            actorsRepository.AddNewActor(a1);
            actorsRepository.AddNewActor(a2);
            actorsRepository.AddNewActor(a3);
            //Act
            var filteredActors = actorsRepository.Get(birthYearBefore: 1995, birthYearAfter: null, name: "Actor");
            //Assert
            Assert.Equal(2, filteredActors.Count());
        }
    }
}
