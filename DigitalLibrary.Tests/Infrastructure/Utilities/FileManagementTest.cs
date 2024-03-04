using System;
using DigitalLibrary.Infrastructure.Utilities;

namespace DigitalLibrary.Tests.Infrastructure.Utilities
{
    public class FileManagementTest
    {
        [Fact]
        public void FileManagement_GetAllFiles_ValidResult()
        {
            //Arrange
            FileManagement fileManagement = new FileManagement();
            int expectedLength = 0;

            //Act
            string[] result = fileManagement.GetAllFiles("../../../Resources/FileManagement_GetAllFiles_ValidResult");

            //Assert
            Assert.Equal(expectedLength, result.Length);
        }

        [Fact]
        public void FileManagement_GetMaxId_ValidResult()
        {
            //Arrange
            FileManagement fileManagement = new FileManagement();
            int expected = 1;

            //Act
            int result = fileManagement.GetMaxId("../../../Resources/FileManagement_GetMaxId_ValidResult");

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1", "HARRY POTTER 6")]
        public void FileManagement_GetFileById_IdExists_ValidResult(string id, string expected)
        {
            //Arrange
            FileManagement fileManagement = new FileManagement();

            //Act
            (string, string) result = fileManagement.GetFileById(id,"../../../Resources/FileManagement_GetFileById_ValidResult");

            //Assert
            Assert.Equal(expected, result.Item1);
        }

        [Theory]
        [InlineData("2")]
        public void FileManagement_GetFileById_IdDoesNotExists_ValidResult(string id)
        {
            //Arrange
            FileManagement fileManagement = new FileManagement();

            //Act & Assert
            Assert.Throws<KeyNotFoundException>(() => fileManagement.GetFileById(id,"../../../Resources/FileManagement_GetFileById_ValidResult"));
        }
    }
}





