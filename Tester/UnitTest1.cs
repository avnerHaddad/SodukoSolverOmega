using System.ComponentModel.DataAnnotations;
using System;
using SodukoSolverOmega.SodukoEngine.Solvers;
using SodukoSolverOmega.Configuration.Exceptions;
using SodukoSolverOmega.Configuration.Consts;

namespace Tester
{
    [TestClass]
    public class UnitTest1
    {
        //test valid 9x9
        [TestMethod]
        public void Test9x9Board1()
        {
            // arange
            string input = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            Consts.setSize(input.Length);
            string output = "831529674796814253542637189159783426483296715627145938365471892274958361918362547";

            // act
            SodukoSolver Solver = new SodukoSolver(input);
            string result = Solver.Solve().ToCleanString();

            // assert
            Assert.AreEqual(output, result);
        }

        // tests Emptyy 9X9 sudoku grid
        [TestMethod]
        public void TestEmpty9x9Board()
        {
            // arange
            string input = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            Consts.setSize(input.Length);
            string output = "198746352365192478247538169512689734736214895489357216821975643653421987974863521";

            // act
            SodukoSolver Solver = new SodukoSolver(input);
            string result = Solver.Solve().ToCleanString();

            // assert
            Assert.AreEqual(output, result);
        }

        // tests valid 16X16 sudoku grid
        [TestMethod]
        public void Test16x16Board()
        {
            // arange 
            string input = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            Consts.setSize(input.Length);
            string output = "15:2349;<@6>?=78>@8=5?7<43129:6;9<47:@618=?;35>236;?2=8>75:94@<1=4>387;:5<261?@98;76412@9:>?<35=<91:=5?634@8>2;7@?259<>31;7=:68462@>;94=?1<587:37=91?235;>8:@<46583;1:<7264@=9?>?:<4>6@8=9372;152358<>:?6794;1=@:7=<@359>8;1642?;1?968=4@25<7>3:4>6@7;12:?=3589<";

            // act
            SodukoSolver Solver = new SodukoSolver(input);
            string result = Solver.Solve().ToCleanString();

            // assert
            Assert.AreEqual(output, result);
        }


        // tests Empty 16X16 sudoku grid
        [TestMethod]
        public void TestEmpty16X16Board()
        {
            // arange
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            Consts.setSize(input.Length);
            string output = "1?@<>3=8:956;742592=1?;@374<>:687;83<46:1?>29=5@46:>9725=8;@1<3?61498@>7523:=?<;2<?7613;9@=845>:>85:42?=71<;39@63@=;:59<46?>2187=41@5;<92:6783?><2>6381?;4957@:=:3782=@6<>1?54;995;?7:4>83@=621<;>31=652@<:9?874?:<2;9716584@>=38764@>:3?=21<;95@=95?<84>;73:621";


            // act
            SodukoSolver Solver = new SodukoSolver(input);
            string result = Solver.Solve().ToCleanString();

            // assert
            Assert.AreEqual(output, result);
        }

        // tests a valid 25X25 sudoku grid
        [TestMethod]
        public void Test25x25Board()
        {
            // arange
            string input = "GF2=5C000@0H67I00B0090>;0<0;09=5GF2@C010I:H67BE?0000?A0;0000F2000030CD0H60I:0600?A00E<>;005G0200@0000@00000I:H0E0A4800>;0F0=0130000070:40000089<00G00=5G0000CD130:H600A00080<>00<0002000F0@CD100:H64BE?A4BE00000900F005010@CI:000I0067E000B00>;0=0G00030CD00:0600?A009000200GF0100CD000C:0070000E0>;00005002=0GF0300D100:00E000B0800089<>002=50000C000000040E0000000>0805G020C01000I:060A4009<0;0000F2@0003000:0000:0000?0;000>0000000030CD03@I0060004BE0>00020000200GF00@0060000B00A4>00000000>G000001000H07000A0BE02=00D00@C007I000E?0<00090;00<00F200D03@0067I000400?A40890>;2=0G00@000H600:060I0A0000>009<G00=500D130000000:H0E?00B90008F205G";
            Consts.setSize(input.Length);
            string output = "GF2=5CD13@:H67IA4BE?9<>;8<>;89=5GF2@CD13I:H67BE?A4BE?A4;89<>F2=5G13@CD:H67I:H67I?A4BE<>;895GF2=3@CD13@CD167I:HBE?A489<>;GF2=513@CDH67I:4BE?A;89<>5GF2=5GF2=@CD13I:H67?A4BE89<>;9<>;82=5GF3@CD17I:H64BE?A4BE?A>;89<GF2=5D13@CI:H67I:H67E?A4B9<>;8=5GF213@CD7I:H6BE?A489<>;2=5GFD13@CD13@C:H67IA4BE?>;89<=5GF2=5GF23@CD17I:H6E?A4B;89<>89<>;F2=5G13@CD67I:HA4BE?A4BE?<>;895GF2=CD13@7I:H6?A4BE9<>;8=5GF2@CD1367I:H67I:H4BE?A;89<>F2=5GCD13@CD13@I:H67?A4BE<>;892=5GF2=5GF13@CD67I:HBE?A4>;89<;89<>GF2=5D13@CH67I:?A4BEF2=5GD13@CH67I:4BE?A<>;89>;89<5GF2=CD13@:H67IE?A4BE?A4B89<>;2=5GF3@CD1H67I:H67I:A4BE?>;89<GF2=5@CD13@CD137I:H6E?A4B9<>;8F2=5G";

            // act
            SodukoSolver Solver = new SodukoSolver(input);
            string result = Solver.Solve().ToCleanString();

            // assert
            Assert.AreEqual(output, result);
        }


        // tests only zeroes 25X25 sudoku grid
        [TestMethod]
        public void TestEmpty25X25SudokuBoard()
        {
            // arange
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            string output = "1?>2;@BG4F3<C7=6EAID58:9HEF<A:1C9D6>85B@?7H4=;G3I2BD34G=I87:1HF?2@<;95>ACE6@85672<HAE9G;4I1:C3>?=BDF=CH9I53;?>6E:DAG2BF81@4<7H12:<CAF8;@5D3BE=97G6?>4ID=IE@41BG7C;2693F>A?H5<:8A4?G9D5:2IE1=>7<68H@B;F3C>;CB3<?69@F4AH8215:IGDE7=6578F3HE>=?:I<GB4D;C21A@9<71DCB=3IA2>G5H4?:E9@F68;9:;HAE71CDB38=F>@<624I5G?52B=?694@87I1E:;HFGA3CD><43GI8>:2F?<A6@;5D1C79EH=BF6E@>;G5H<4D9C?83I=B721A:3G:1=F;@EC5?BA>7I2<6849HD7IF5D82<19=6HGCA;4@:E3?B>2A9?B:4I=H8731<DCG>EF6;5@8<4>E76A3B;2@FD95?1H=:ICG;@6CH?>D5G:94IE=83BF<721AGBD3198762HFE:5IA@?<C>=;4CHAF2GE>;1I=<86:9754DB@?3?>=<4IFCB3A@721HGED;:9865:9875A@=<4DB?;3C>621IHGFEIE@;6HD?:5GC>94FB=83A<721";
            Consts.setSize(input.Length);

            // act
            SodukoSolver Solver = new SodukoSolver(input);
            string result = Solver.Solve().ToCleanString();

            // assert
            Assert.AreEqual(output, result);
        }


        // tests a sudoku grid with a duplication in row, column or cell
        [TestMethod]
        public void TestInvalidIn9X9Board()
        {
            // arange
            
            string input = "880000070006010053040600000000080400003000700020005038000000800004050061900002000";
            Consts.setSize(input.Length);


            // act
            SodukoSolver Solver = new SodukoSolver(input);


            // assert
            Assert.ThrowsException<UnsolvableSudokuException>(() => Solver.Solve());
        }
    }
}