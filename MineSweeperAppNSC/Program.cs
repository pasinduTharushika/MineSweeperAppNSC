using MineSweeperAppNSC.Class;
using MineSweeperAppNSC.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperAppNSC
{
    class Program
    {
       
        public static void Main(string[] args)
        {
            var insminefld = new Minefield();
            var insgrd= new Grid(insminefld);
            var program = new MineSweeperGame(insminefld, insgrd);
            program.PlayGame();
        }
    }
}
