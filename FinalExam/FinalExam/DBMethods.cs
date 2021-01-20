using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalExam
{
    public class DBMethods
    {
        public void ExportTeams(ref List<Team> teams)
        {
            using (BloggingDbContext db = new BloggingDbContext())
            {
                foreach (var team in teams)
                {
                    db.Teams.Add(team);
                }
                db.SaveChanges();

            }
        }

        public List<Team> ImportTeams()
        {
            using (BloggingDbContext db = new BloggingDbContext())
            {
                return new List<Team>(db.Teams);
            }
        }

        public void ExportTeamMatches(ref List<TeamMatch> teamMatches)
        {
            using (BloggingDbContext db = new BloggingDbContext())
            {
                foreach (var teamMatch in teamMatches)
                {
                    db.TeamMatches.Add(teamMatch);
                }
                db.SaveChanges();
            }
        }

        public void ShowData()
        {
            using (BloggingDbContext db = new BloggingDbContext())
            {
                List<Team> teams = new List<Team>(db.Teams.OrderBy(t => t.Place));
                Console.WriteLine();
                foreach (var team in teams)
                {
                    Console.WriteLine(team.Place + ".\t" + team.Name + "\t" + team.Point + " points");
                }
                Console.WriteLine();
            }
        }

        public void WipeData()
        {
            using (BloggingDbContext db = new BloggingDbContext())
            {
                List<Team> teams = new List<Team>(db.Teams);
                List<TeamMatch> teamMatches = new List<TeamMatch>(db.TeamMatches);
                db.Teams.RemoveRange(teams);
                db.TeamMatches.RemoveRange(teamMatches);
                db.SaveChanges();
            }
        }
    }
}
