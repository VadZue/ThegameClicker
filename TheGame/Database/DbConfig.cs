using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace TheGame.Database;

public class GameDbContext : DbContext
{
    private static GameDbContext _db;
    public static GameDbContext Db => _db ??= new();
    public GameDbContext() => Database.EnsureCreated();

    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<UsersSkills> UsersSkills { get; set; }

    public string DbPath { get; } = "GameStore.db";

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      
        options.UseSqlite($"Data Source={DbPath}");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Skill>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<UsersSkills>().Property(x => x.Id).ValueGeneratedOnAdd();
    }
}

public class User
{

    public int Id { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string NikName { get; set; }
    public virtual List<UsersSkills> Skills { get; set; }
}


public class Skill
{

    public int Id { get; set; }
    public float MoneyUp { get; set; }
    public float ScoreUp { get; set; }

}

public class UsersSkills
{
    public int Id { get; set; }

    [ForeignKey("UserKey")]
    public User User { get; set; }

    [ForeignKey("SkillKey")]
    public Skill Skill { get; set; }
    public int Lvl { get; set; }

}
