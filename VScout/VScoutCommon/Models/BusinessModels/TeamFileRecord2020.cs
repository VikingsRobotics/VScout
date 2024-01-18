using System;
using System.Collections.Generic;
using System.Linq;

namespace VScoutCommon.Models.BusinessModels
{
    public class TeamFileRecord2020 : TeamFileRecordBase
    {
        public List<Round> Rounds { get; } = new List<Round>();

        public bool HasData
        {
            get
            {
                if (Rounds.Count == 0) return false;

                return Rounds.Any(round => round.HasData);
            }
        }

        public int PostionControlCount
        {
            get
            {
                return Rounds.Count(m => m.ControlPanel.AchievedPositionControl);
            }
        }

        public int RotationControlCount
        {
            get
            {
                return Rounds.Count(m => m.ControlPanel.AchievedRotationControl);
            }
        }

        public decimal AveragePowerCellPoints
        {
            get
            {
                if (Rounds.Count == 0) return 0;

                return Math.Round(Rounds.Sum(m => m.TotalPowerPort) / (decimal)Rounds.Count, 2, MidpointRounding.AwayFromZero);
            }
        }

        public int ClimbCount
        {
            get
            {
                return Rounds.Count(m => m.Endgame.AchievedClimb);
            }
        }

        public int BalanceCount
        {
            get
            {
                return Rounds.Count(m => m.Endgame.AchievedBalance);
            }
        }

        public string Comments
        {
            get
            {
                string comments = string.Empty;
                foreach (Round round in Rounds)
                {
                    comments += round.Comments;
                }

                return comments;
            }
        }

        public class Round
        {
            public int Number { get; set; }

            public bool HasData
            {
                get
                {
                    return this.GoThroughCity || this.GoUnderTurntable || this.Autonomous.Moved ||
                           this.TotalPowerPort > 0 || this.ControlPanel.AchievedPositionControl || this.ControlPanel.AchievedRotationControl ||
                           this.Endgame.AchievedClimb || this.Endgame.AchievedBalance || this.Endgame.GetBackDown || (this.Comments.Length > 0);
                }
            }

            public bool GoUnderTurntable { get; set; }

            public bool GoThroughCity { get; set; }

            public AutonomousType Autonomous { get; } = new AutonomousType();
            public class AutonomousType
            {
                public bool Moved { get; set; }

                public PowerPortType PowerPort { get; } = new PowerPortType();
            }

            public int TotalPowerPort
            {
                get
                {
                    return PowerPort.TotalPort + Autonomous.PowerPort.TotalPort;
                }
            }

            public PowerPortType PowerPort { get; } = new PowerPortType();
            public class PowerPortType
            {
                public int TotalPort
                {
                    get
                    {
                        return LowGoal + HighGoal;
                    }
                }
                public int LowGoal { get; set; }
                public int HighGoal { get; set; }
            }

            public ControlPanelType ControlPanel { get; } = new ControlPanelType();
            public class ControlPanelType
            {
                public bool AchievedRotationControl { get; set; }
                public bool AchievedPositionControl { get; set; }
            }

            public EndgameType Endgame { get; } = new EndgameType();
            public class EndgameType
            {
                public bool AchievedClimb { get; set; }
                public bool AchievedBalance { get; set; }
                public bool GetBackDown { get; set; }
            }

            public string Comments { get; set; }
        }

    }
}
