using CMMFileDatabase.FileDatabase;
using CMMToolBoxNUnitTests.FileDatabase.TestModels;

namespace CMMToolBoxNUnitTests
{
    [TestFixture]
    public class FileDatabaseManager_GetNewest
    {
        FileDatabaseManager _fileDB;
        TestingModel _ModelOne;
        TestingModel _ModelTwo;
        Guid _guid;
        string _id = "55d15758-00a4-4522-96b5-217e64267360";

        [SetUp]
        public void Setup()
        {
            _fileDB = new FileDatabaseManager(new FileDBRepositorySettings());
            _guid = new Guid(_id);
            _ModelOne = new TestingModel
            {
                Id = _guid,
                Message = "Model One"
            };

            _ModelTwo = new TestingModel
            {
                Id = Guid.NewGuid(),
                Message = "Model Two"
            };
        }

        [Test]
        public void Get_Newest()
        {
            // Arrange
            _fileDB.DeleteAllEntries<TestingModel>();
            _fileDB.Upsert(_ModelOne);
            _fileDB.Upsert(_ModelTwo);

            // Act
            var result = _fileDB.GetNewest<TestingModel>();

            // Assert
            if (result is null)
            {
                Assert.Fail();
            }
            else 
            {
                Assert.IsTrue(result.Id == _ModelTwo.Id);
            }
        }
    }
}