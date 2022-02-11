using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Beacon
    {


        public Point RelativePosition { get; set; }

        public Point WorldPosition { get; set; }



        public Beacon(
            in Int32 positionX, in Int32 positionY, in Int32 positionZ)
        {
            RelativePosition = new Point(in positionX, in positionY, in positionZ);
            WorldPosition = new Point(in positionX, in positionY, in positionZ);
        }

        //public void UpdateWorldPosition()
        //{
        //    WorldPosition.X = RelativePosition.X - Parent.Position.X;
        //    WorldPosition.Y = RelativePosition.Y - Parent.Position.Y;
        //    WorldPosition.Z = RelativePosition.Z - Parent.Position.Z;
        //}
    }
}
