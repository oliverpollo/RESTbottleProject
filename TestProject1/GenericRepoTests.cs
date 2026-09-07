using RESTbottle.Models;
using RESTbottle.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestProject1
{
    public class UnitTestGenericRepo
    {
        [Fact]
        public async Task TestAddAndGetAll()
        {
            //Arrange
            GenericRepo<Bottle> genericRepo = new GenericRepo<Bottle>();
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            await genericRepo.Add(b1);
            await genericRepo.Add(b2);
            //Act
            var allBottles = await genericRepo.GetAll();
            //Assert
            Assert.Equal(2, allBottles.Count());
        }

        [Fact]

        public void TestDeleteMethodInGenericRepository()
        {
            //Arrange
            GenericRepo<Bottle> genericRepo = new GenericRepo<Bottle>();
            genericRepo.Add(new Bottle { Volume = 500, Name = "Bottle 1" });
            genericRepo.Add(new Bottle { Volume = 750, Name = "Bottle 2" });
            //Act
            var deleteTask = genericRepo.Delete(1);
            //Assert
            Assert.NotNull(deleteTask);
        }

        [Fact]

        public void TestGetAll()
        {
            //Arrange
            GenericRepo<Bottle> genericRepo = new GenericRepo<Bottle>();
            genericRepo.Add(new Bottle { Volume = 500, Name = "Bottle 1" });
            genericRepo.Add(new Bottle { Volume = 750, Name = "Bottle 2" });
            //Act
            var allBottles = genericRepo.GetAll();
            //Assert
            Assert.Equal(2, allBottles.Result.Count());
        }
        [Fact]
        public void TestGetById()
        {
            //Arrange
            GenericRepo<Bottle> genericRepo = new GenericRepo<Bottle>();
            genericRepo.Add(new Bottle { Volume = 500, Name = "Bottle 1" });
            genericRepo.Add(new Bottle { Volume = 750, Name = "Bottle 2" });
            //Act
            var bottleById = genericRepo.GetById(1);
        }
        //[Fact]

        //public void TestUpdateMethodWhereEntityExists()
        //{
        //    //Arrange
        //    GenericRepo<Bottle> genericRepo = new GenericRepo<Bottle>();
        //    genericRepo.Add(new Bottle { Volume = 500, Name = "Bottle 1" });
        //    //Act
        //    var bottleToUpdate = genericRepo.Update(new Bottle { Id = 1, Volume = 750, Name = "Updated Bottle" });
        //    //Assert
        //    Assert.NotNull(bottleToUpdate);
        //}

        [Fact]      

      
        
        public void TestUpdateMethodWhereEntityDoesNotHaveId()
        {
            //Arrange
            GenericRepo<Bottle> genericRepo = new GenericRepo<Bottle>();
            genericRepo.Add(new Bottle { Volume = 500, Name = "Bottle 1" });
            //Act
            var bottleToUpdate = genericRepo.Update(new Bottle { Volume = 750, Name = "Updated Bottle" });

            //Assert
            Assert.NotNull(bottleToUpdate);
        }     

        
    }
}
