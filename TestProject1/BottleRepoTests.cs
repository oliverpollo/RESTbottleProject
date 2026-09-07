using Microsoft.EntityFrameworkCore;
using RESTbottle.EFCore;
using RESTbottle.Models;
using RESTbottle.Repos;
using System;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace TestProject1
{
    public class UnitTestsOfBottlesRepository
    {

        private bool useDatabase = true; // Set to true to use BottlesRepositoryDatabase, false to use BottlesRepositoryList
        private IBottlesRepository repo;
        public UnitTestActorsRepository()
        {
            // You can set the useDatabase variable here based on your testing needs
            // For example, you can read from a configuration file or environment variable
            if (useDatabase) 
            {
                var optionsBuilder = new DbContextOptionsBuilder<BottlesDBContext>();
                // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets
                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
                // connection string structure
                //   "Data Source=mssql7.unoeuro.com;Initial Catalog=FROM simply.com;Persist Security Info=True;User ID=FROM simply.com;Password=DB PASSWORD FROM simply.com;TrustServerCertificate=True"
                BottlesDBContext _dbContext = new(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Bottles");
                repo = new BottlesRepositoryDatabase(_dbContext);

            }
            else 
            { 
                repo = new BottlesRepositoryList ();
            }
        }

        [Fact]
        public void TestGetBottleById_Returns_Correct_Bottle()
        {
            //Arrange
            
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            //Act
            var bottleById = bottlesRepository.GetBottleById(2);
            //Assert
            Assert.Equal(b2, bottleById);
        }

        [Fact]
        public void TestAdd()
        {
            //Arrange
            IBottlesRepository bottlesRepository = new BottlesRepositoryList();

            Bottle b = new Bottle { Volume = 500, Name = "Test Bottle" };

            //Act

            Bottle addedBottle = bottlesRepository.AddBottle(b);

            //Assert

            Assert.Equal(1, addedBottle.Id);
        }

        [Fact]
        public void TestGetMethodWithBottles()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Bottle 3" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var allBottles = bottlesRepository.Get();
            //Assert
            Assert.Equal(3, allBottles.Count());
        }

        //Constructor tests for BottlesRepository
        [Fact]

        public void TestBottleConstructorWithTestData()
        {
            //Arrange
            IBottlesRepository bottlesRepository = new BottlesRepositoryList(includesTestData: true);
            //Act
            var allBottles = bottlesRepository.GetAllBottles();
            //Assert
            Assert.Equal(3, allBottles.Count());
        }

        [Fact]
        public void TestBottleConstructorWithoutTestData()
        {
            //Arrange
            IBottlesRepository bottlesRepository = new BottlesRepositoryList(includesTestData: false);
            //Act
            var allBottles = bottlesRepository.GetAllBottles();
            //Assert
            Assert.NotNull(allBottles);
            Assert.Empty(allBottles);
        }

        [Fact]

        public void TestConstructorWithDefaultValue()
        {
            //Arrange
            IBottlesRepository bottlesRepository = new BottlesRepositoryList();
            //Act
            var allBottles = bottlesRepository.GetAllBottles();
            //Assert
            Assert.NotNull(allBottles);
            Assert.Empty(allBottles);
        }

        [Fact]

        public void TestGetMethodWithNameStartsWith()
        {
            //Arrange
            IBottlesRepository bottlesRepository = new BottlesRepositoryList();
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var filteredBottles = bottlesRepository.Get(nameStartsWith: "Bottle");
            //Assert
            Assert.Equal(2, filteredBottles.Count());
        }
        [Fact]

        public void TestGetMethod_WhereNameStartsWith_isNull()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var filteredBottles = bottlesRepository.Get(nameStartsWith: null);
            //Assert
            Assert.Equal(3, filteredBottles.Count());
        }

        //Test CRUD operations for BottlesRepository

        [Fact]

        public void TestDeleteBottleById_Returns_Correct_Bottle()
        {
            //Arrange
            Bottle b = new Bottle { Volume = 500, Name = "Test Bottle" };
            Bottle addedBottle = bottlesRepository.AddBottle(b);
            //Act
            Bottle? deletedBottle = bottlesRepository.DeleteByIdBottle(addedBottle.Id);
            //Assert
            Assert.NotNull(deletedBottle);
            Assert.Equal(addedBottle.Id, deletedBottle?.Id);

        }

        [Fact]

        public void TestDeleteBottleById_Returns_Null_For_Nonexistent_Bottle()
        {
            //Arrange
            //Act
            Bottle? deletedBottle = bottlesRepository.DeleteByIdBottle(999); // Non-existent ID
            //Assert
            Assert.Null(deletedBottle);
        }

        [Fact]

        public void TestUpdateBottle_Returns_Updated_Bottle()
        {
            //Arrange
            Bottle bottleToUpdate = new Bottle { Volume = 500, Name = "Old Name" };
            Bottle addedBottle = bottlesRepository.AddBottle(bottleToUpdate);
            //Act
            Bottle updatedBottleInfo = new Bottle { Volume = 750, Name = "New Name" };
            Bottle? updatedBottle = bottlesRepository.UpdateBottle(addedBottle.Id, updatedBottleInfo);
            //Assert
            Assert.NotNull(updatedBottle);
            Assert.Equal(addedBottle.Id, updatedBottle?.Id);
            Assert.Equal(updatedBottleInfo.Volume, updatedBottle?.Volume);
            Assert.Equal(updatedBottleInfo.Name, updatedBottle?.Name);
        }

        [Fact]

        public void TestGetV2WhereMinVolumeIsNullAndNameStartsWithIsNull()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var filteredBottles = bottlesRepository.GetV2(nameStartsWith: null, minVolume: null, sortOrder: "volume_desc");
            //Assert
            Assert.Equal(3, filteredBottles.Count());
        }

        [Fact]

        public void TestGetV2MethodWithFiltersAndSortingWhereMinVolumeIsNull()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var filteredBottles = bottlesRepository.GetV2(nameStartsWith: "Bottle", minVolume: null, sortOrder: "volume_desc");
            //Assert
            Assert.Equal(2, filteredBottles.Count());
        }

        [Fact]

        public void TestGetV2MethodWithFiltersAndSortingWhereMinVolumeIsNotNull()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var filteredBottles = bottlesRepository.GetV2(nameStartsWith: "Bottle", minVolume: 600, sortOrder: "volume_desc");
            //Assert
            Assert.Equal(1, filteredBottles.Count());
        }
        [Fact]

        public void TestGetV2MethodWithFiltersAndSortingWhereNameStartsWithIsNull()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act
            var filteredBottles = bottlesRepository.GetV2(nameStartsWith: null, minVolume: 600, sortOrder: "volume_desc");
            //Assert
            Assert.Equal(2, filteredBottles.Count());
        }
        //[Fact]

        //public void TestGetV2MethodWithFiltersAndSortingWhereSortOrderIsNull()
        //{
        //    //Arrange
        //    BottlesRepository bottlesRepository = new BottlesRepository();
        //    Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
        //    Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
        //    Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
        //    bottlesRepository.AddBottle(b1);
        //    bottlesRepository.AddBottle(b2);
        //    bottlesRepository.AddBottle(b3);
        //    //Act
        //    var filteredBottles = bottlesRepository.GetV2(nameStartsWith: "Bottle", minVolume: 600, sortOrder: null);
        //    //Assert
        //    Assert.Equal(1, filteredBottles.Count());
        //}

        [Fact]

        public void TestGetV2MethodWithFiltersAndSortingWhereSortOrderIsInvalid()
        {
            //Arrange
            Bottle b1 = new Bottle { Volume = 500, Name = "Bottle 1" };
            Bottle b2 = new Bottle { Volume = 750, Name = "Bottle 2" };
            Bottle b3 = new Bottle { Volume = 1000, Name = "Test Bottle" };
            bottlesRepository.AddBottle(b1);
            bottlesRepository.AddBottle(b2);
            bottlesRepository.AddBottle(b3);
            //Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => bottlesRepository.GetV2(nameStartsWith: "Bottle", minVolume: 600, sortOrder: "invalid_sort_order"));
            Assert.Equal("Invalid sortOrder value: invalid_sort_order. Valid values are: name_asc, name_desc, volume_asc, volume_desc.", exception.Message);

        }

    }
}

