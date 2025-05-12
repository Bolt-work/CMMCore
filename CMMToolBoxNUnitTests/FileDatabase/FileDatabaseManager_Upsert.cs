using CMMFileDatabase.FileDatabase;
using CMMToolBoxNUnitTests.FileDatabase.TestModels;

namespace CMMToolBoxNUnitTests
{
    [TestFixture]
    public class FileDatabaseManager_Upsert
    {
        FileDatabaseManager _fileDB;
        TestingModel _ModelOne;
        string _id = "55d15758-00a4-4522-96b5-217e64267360";

        [SetUp]
        public void Setup()
        {
            _fileDB = new FileDatabaseManager(new FileDBRepositorySettings());
            _ModelOne = new TestingModel
            {
                Id = new Guid(_id),
                Message = "Model One"
            };
        }

        [Test]
        public void Upsert_Model()
        {
            // Arrange
            _fileDB.DeleteAllEntries<TestingModel>();

            // Act
            _fileDB.Upsert(_ModelOne);

            // Assert
            var results = _fileDB.GetAllEntries<TestingModel>();
            Assert.IsTrue(results.Any());
        }
    }
}