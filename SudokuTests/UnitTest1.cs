using SodukoSolverOmega.SodukoEngine.Solvers;

namespace SudokuTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            string input = "0000000000";
            string output = "0";
            SodukoSolver sudoku = new SodukoSolver(input);
            // act
            string result = sudoku.Solve().ToCleanString();
            // assert
            Assert.AreEqual(result,output);
            //

        }
    }
}