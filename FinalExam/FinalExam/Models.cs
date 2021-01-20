using System;
using System.Collections.Generic;
using System.Text;

namespace FinalExam
{
    public class Team
    {
        public int TeamID { get; set; }

        public string Name { get; set; }

        public int Point { get; set; }

        public int? Place { get; set; }

        public Team(string name, int TeamID)
        {
            this.Name = name;
            this.TeamID = TeamID;
            this.Point = 0;
        }
    }

    public class TeamMatch
    {
        public int Team1ID { get; set; }

        public int Team2ID { get; set; }

        public string Status { get; set; }
    }

    public class Tournament
    {
        public Tournament(ref List<Team> teams, ref List<TeamMatch> teamMatches)
        {
            int teamsCount = teams.Count;

            for (int i = 1; i < teamsCount; i++)
            {
                for (int j = i + 1; j <= teamsCount; j++)
                {
                    teamMatches.Add(new TeamMatch { Team1ID = i, Team2ID = j });
                }
            }

        }
    }
}
