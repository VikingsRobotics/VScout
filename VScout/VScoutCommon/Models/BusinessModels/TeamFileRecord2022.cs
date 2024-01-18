using System;
using System.Collections.Generic;
using System.Linq;

namespace VScoutCommon.Models.BusinessModels
{
    public class TeamFileRecord2022 : TeamFileRecordBase
    {
        public List<Round> Rounds { get; } = new List<Round>();

        public decimal AverageHubPoints
        {
            get
            {
                if (Rounds.Count == 0) return 0;

                return Math.Round(Rounds.Sum(m => m.TotalHub) / (decimal)Rounds.Count, 2, MidpointRounding.AwayFromZero);
            }
        }

        public int LowBarCount
        {
            get
            {
                return Rounds.Count(m => m.Endgame.LowRung);
            }
        }

        public int SecondBarCount
        {
            get
            {
                return Rounds.Count(m => m.Endgame.SecondRung);
            }
        }

        public int ThirdBarCount
        {
            get
            {
                return Rounds.Count(m => m.Endgame.ThirdRung);
            }
        }

        public int HighBarCount
        {
            get
            {
                return Rounds.Count(m => m.Endgame.HighRung);
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
                    return Autonomous.Moved || TotalHub > 0 || Endgame.LowRung || Endgame.SecondRung || Endgame.ThirdRung || Endgame.HighRung || (Comments.Length > 0);
                }
            }

            public AutonomousType Autonomous { get; } = new AutonomousType();
            public class AutonomousType
            {
                public bool Moved { get; set; }

                public HubType Hub { get; } = new HubType();
            }

            public int TotalHub
            {
                get
                {
                    return Hub.Total + Autonomous.Hub.Total;
                }
            }

            public HubType Hub { get; } = new HubType();
            public class HubType
            {
                public int Total
                {
                    get
                    {
                        return LowGoal + HighGoal;
                    }
                }
                public int LowGoal { get; set; }
                public int HighGoal { get; set; }
            }

            public EndgameType Endgame { get; } = new EndgameType();
            public class EndgameType
            {
                public bool LowRung { get; set; }
                public bool SecondRung { get; set; }
                public bool ThirdRung { get; set; }
                public bool HighRung { get; set; }
            }

            public string Comments { get; set; }
        }
    }
}
