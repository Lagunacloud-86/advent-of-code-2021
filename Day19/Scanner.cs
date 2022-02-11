using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Scanner
    {
        public String Name { get; set; }
        public Point Position { get; set; }

        //public Point Orientation { get; set; }


        public Scanner(
            in String name,
            in Int32 positionX, in Int32 positionY, in Int32 positionZ)
        {
            Name = name;
            Position = new Point(in positionX, in positionY, in positionZ);
            //Orientation = new Point(in orientationX, in orientationY, in orientationZ);
        }


    }
}
