using CMMFileDatabase.FileDatabase;
using CMMToolBoxNUnitTests.FileDatabase.TestModels;

namespace CMMToolBoxNUnitTests
{
    [TestFixture]
    public class FileDatabaseManager_GetEntryById
    {
        FileDatabaseManager _fileDB;
        TestingModel _ModelOne;
        Guid _guid;
        string _id = "55d15758-00a4-4522-96b5-217e64267360";

        [SetUp]
        public void Setup()
        {
            _guid = new Guid(_id);
            _fileDB = new FileDatabaseManager(new FileDBRepositorySettings());
            _ModelOne = new TestingModel
            {
                Id = new Guid(_id),
                Message = "Model One"
            };
        }

        [Test]
        public void Get_Entry_By_Id()
        {
            // Arrange
            _fileDB.DeleteAllEntries<TestingModel>();
            _fileDB.Upsert(_ModelOne);

            // Act
            var result = _fileDB.EntryWithIdExists<TestingModel>(_guid);

            // Assert
            Assert.IsTrue(result);
        }
    }
}