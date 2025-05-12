using CMMFileDatabase.FileDatabase;
using CMMToolBoxNUnitTests.FileDatabase.TestModels;

namespace CMMToolBoxNUnitTests
{
    [TestFixture]
    public class FileDatabaseManager_GetByKeyStr
    {
        FileDatabaseManager _fileDB;
        TestingModel _ModelOne;
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
        }

        [Test]
        public void Get_By_Key_Str()
        {
            // Arrange
            _fileDB.DeleteAllEntries<TestingModel>();
            _fileDB.Upsert(_ModelOne);

            // Act
            var results = _fileDB.GetByKeyStr<TestingModel>("Message", "Model One");

            // Assert
            Assert.IsTrue(results.Any());
        }
    }
}