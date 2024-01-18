using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VScoutCommon.Models.BusinessModels
{
    public class TeamFileRecord
    {
        public int TeamNumber { get; set; }
        public decimal Opr { get; set; }
        public int Rank { get; set; }

        public List<Round> Rounds { get; } = new List<Round>();
        public class Round
        {
            public int Number { get; set; }

            public AutonomousType Autonomous { get; } = new AutonomousType();
            public class AutonomousType
            {
                public bool Moved { get; set; }
                public bool Camera { get; set; }
                public bool DescendedPlatform { get; set; }
            }

            public HatchesType Hatches { get; } = new HatchesType();
            public class HatchesType
            {
                public int CargoShip { get; set; }
                public int Level1 { get; set; }
                public int Level2 { get; set; }
                public int Level3 { get; set; }
            }

            public CargoType Cargo { get; } = new CargoType();
            public class CargoType
            {
                public int CargoShip { get; set; }
                public int Level1 { get; set; }
                public int Level2 { get; set; }
                public int Level3 { get; set; }
            }

            public string Comments { get; set; }
        }
    }
}
