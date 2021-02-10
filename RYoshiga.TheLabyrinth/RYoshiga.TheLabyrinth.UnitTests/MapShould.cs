using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Shouldly;
using Xunit;

namespace RYoshiga.TheLabyrinth.UnitTests
{
    public class MapShould
    {
        [Fact]
        public void NotTakeToolong()
        {
            var row = new string('O', 900);
            var map = new Map(900, 400);

            for (int i = 0; i < 400; i++)
            {
                map.AddRow(i, row);    
            }

            var bfsResolver = new BfsResolver(map);

            var sw = new Stopwatch();
            sw.Start();
            
            bfsResolver.GetResult(new Point(43, 54)).ShouldBe(360000);
            
            sw.Stop();

            sw.ElapsedMilliseconds.ShouldBeLessThan(1000);
        }

    }
}
