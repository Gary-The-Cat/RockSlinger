using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid_SFML.CrateTools
{
    public class Crate
    {
        public CrateType CrateType { get; set; }

        public bool IsCrate { get; set; }

        public Vector2f Centroid { get; set; }

    }

    public enum CrateType
    {
        Full,
        Broken,
        Tatters
    }
}
