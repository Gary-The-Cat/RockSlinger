using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid_SFML.CrateTools
{
    public class CrateManager
    {
        public readonly int VerticalPixelOffset = 100;
        public int CrateSize = 128;

        private Crate[,] crates;

        public CrateManager()
        {
            crates = new Crate[15, 8];
        }

        public Vector2i GetCrateIndexFromPosition(Vector2f position)
        {
            var x = position.X;
            var y = position.Y - VerticalPixelOffset;

            return new Vector2i((int)(x / CrateSize), (int)(y / CrateSize));
        }

        public Vector2f GetCrateCentreFromIndex(Vector2i index)
        {
            var x = index.X * CrateSize + CrateSize / 2;
            var y = VerticalPixelOffset + index.Y * CrateSize + CrateSize / 2;

            return new Vector2f(x, y);
        }

        public Crate[,] GetCrates()
        {
            return crates;
        }

        public void AddCrate(Vector2f cratePosition)
        {
            var crateIndex = GetCrateIndexFromPosition(cratePosition);
            if(crates[crateIndex.X, crateIndex.Y] == null)
            {
                crates[crateIndex.X, crateIndex.Y] = new Crate() { CrateType = CrateType.Full, IsCrate = true, Centroid = GetCrateCentreFromIndex(crateIndex) };
            }
            else
            {
                crates[crateIndex.X, crateIndex.Y].CrateType++;
                if ((int)crates[crateIndex.X, crateIndex.Y].CrateType > 2)
                {
                    crates[crateIndex.X, crateIndex.Y].CrateType = CrateType.Full;
                }
            }
        }
    }
}
