using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalExam
{
    class Program
    {
        static void Main(string[] args)
        {
            //DBMethods db1 = new DBMethods();
            //db1.WipeData();
            //return;

            List<Team> teams = new List<Team>();
            if (!teams.CreateTeams()) return;
            
            List<TeamMatch> teamMatches = new List<TeamMatch>();
            Tournament tournament = new Tournament(ref teams, ref teamMatches);
            tournament.HoldTournament(ref teams, ref teamMatches);
            tournament.GetResult(ref teams, ref teamMatches);

            DBMethods db = new DBMethods();
            db.ExportTeams(ref teams);
            db.ExportTeamMatches(ref teamMatches);

            db.ShowData();
        }
    }

    public class BloggingDbContext : DbContext
    {
        public DbSet<Team> Teams { set; get; }

        public DbSet<TeamMatch> TeamMatches { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=RGN7_64-PC\\SQLEXPRESS;database=TSU_DB_local;Integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasKey(k => k.TeamID);
            modelBuilder.Entity<Team>().Property(t => t.TeamID).ValueGeneratedNever();
            modelBuilder.Entity<TeamMatch>().HasKey(k => new { k.Team1ID, k.Team2ID });
        }
    }
}
