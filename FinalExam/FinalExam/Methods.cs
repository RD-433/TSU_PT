using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FinalExam
{
    static public class Methods
    {
        public static bool CreateTeams(this List<Team> teams)
        {
            Console.WriteLine("Type teams' name and press Enter to create it (min 5), \nor leave it empty to finish.\n");
            int count = 0;
            while (true)
            {
                string name = Console.ReadLine();
                if (name == "") break;
                if (name.CheckNameIsEmpty() || name.CheckNameIsNumber()) continue;
                count++;
                teams.Add(new Team(name, count));
            }

            if (teams.Count < 5)
            {
                Console.WriteLine("Too few teams, maybe next time?");
                return false;
            }
            return true;
        }

        private static bool CheckNameIsEmpty(this string name)
        {
            if (name.Length == 0) return true;
            name = name.ClearString();
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != ' ') return false;
            }
            return true;
        }

        private static bool CheckNameIsNumber(this string name)
        {
            try
            {
                int nameInt = Convert.ToInt32(name.ClearString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static string ClearString(this string name)
        {
            return name.Replace(".", "").Replace(",", "").Replace("_", "").Replace("-", "");
        }

        public static void HoldTournament(this Tournament tournament, ref List<Team> teams, ref List<TeamMatch> teamMatches)
        {
            Console.WriteLine("Type for each match \"W\" for \"Win\", \"L\" for \"Lose\" and \"D\" for \"Draw\"\n");

            foreach (var teamMatch in teamMatches)
            {
                Console.Write(teams.Where(t => t.TeamID == teamMatch.Team1ID).First().Name + "\t" + teams.Where(t => t.TeamID == teamMatch.Team2ID).First().Name + "\t");

                while (true)
                {
                    string status = Console.ReadLine();
                    if (status == "W" || status == "w")
                    {
                        teamMatch.Status = "Win";
                        break;
                    }
                    else if (status == "L" || status == "l")
                    {
                        teamMatch.Status = "Lose";
                        break;
                    }
                    else if (status == "D" || status == "d")
                    {
                        teamMatch.Status = "Draw";
                        break;
                    }
                    else
                    {

                    }
                }
            }
        }

        public static void GetResult(this Tournament tournament, ref List<Team> teams, ref List<TeamMatch> teamMatches)
        {
            foreach (var teamMatch in teamMatches)
            {
                int point1 = 0, point2 = 0;
                if (teamMatch.Status == "Win")
                {
                    point1 = 3; point2 = -1;
                }
                else if (teamMatch.Status == "Lose")
                {
                    point1 = -1; point2 = 3;
                }
                else if (teamMatch.Status == "Draw")
                {

                }
                else
                {

                }
                teams.Where(t => t.TeamID == teamMatch.Team1ID).First().Point += point1;
                teams.Where(t => t.TeamID == teamMatch.Team2ID).First().Point += point2;
            }

            teams = teams.SortByPoints().SortByNames().SetPlaces();
        }

        public static void DisplayTeams(this List<Team> teams)
        {
            foreach (var team in teams)
            {
                Console.WriteLine(team.Place + "\t" + team.Name + "\t" + team.Name + "\t" + team.Point);
            }
        }

        private static List<Team> SortByPoints(this List<Team> teams)
        {
            return teams.OrderByDescending(t => t.Point).ToList<Team>();
        }

        private static List<Team> SortByNames(this List<Team> teams)
        {
            List<Team> teamTemp = new List<Team>();
            for (int i = 0; i < teams.Count - 1; i++)
            {
                if (teams[i].Name == teams[i + 1].Name)
                {
                    teamTemp.Add(teams[i]);
                    int j = i;
                    while (teams[j].Name == teams[j + 1].Name)
                    {
                        teamTemp.Add(teams[j + 1]);
                        j++;
                    }
                    teamTemp = teamTemp.OrderByDescending(t => t.Name).ToList<Team>();

                    foreach (var team in teamTemp)
                    {
                        teams[i] = team;
                        i++;
                    }
                }
            }
            return teams;
        }

        private static List<Team> SetPlaces(this List<Team> teams)
        {
            int i = 1;
            foreach (var team in teams)
            {
                team.Place = i++;
            }
            return teams;
        }

    }
}

