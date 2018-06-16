using System.IO;
using WordGuess;
using Xunit;

namespace WordGuessTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("TEST", "E", "_ E _ _")]
        [InlineData("TEST", "A", "_ _ _ _")]
        public void TestGuessLetter(string word, string guessed, string expected)
        {
            Assert.Equal(expected, Program.BlankWord(word, guessed));
        }

        [Fact]
        public void TestDeleteFile()
        {
            if (!File.Exists(Program.path)) Program.CreateFile();
            Program.DeleteFile();
            Assert.False(File.Exists(Program.path));
        }

        [Fact]
        public void TestCreateFile()
        {
            if (File.Exists(Program.path)) Program.DeleteFile();
            Program.CreateFile();
            Assert.True(File.Exists(Program.path));
        }

        [Fact]
        public void TestReadFile()
        {
            if (!File.Exists(Program.path)) Program.CreateFile();
            Assert.True(Program.ReadFile().Length > 0);
        }

        [Theory]
        [InlineData("skeleton")]
        [InlineData("aspiration")]
        public void TestUpdateFile(string word)
        {
            if (!File.Exists(Program.path)) Program.CreateFile();
            Program.UpdateFile(word);
            string[] myFile = Program.ReadFile();
            Assert.True(Program.ArrayHasWord(myFile, word));
        }
    }
}
