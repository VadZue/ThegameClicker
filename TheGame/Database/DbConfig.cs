using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace TheGame.Database;

public sealed class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) 
    {
        _ = Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .OwnsOne(x => x.MoneySkill, skill =>
            {
                skill.Property(s => s.Up).HasDefaultValue(1);
                skill.Property(s => s.UpgradeCost).HasDefaultValue(1000);
            });

        modelBuilder.Entity<User>()
            .OwnsOne(x => x.ScoreSkill, skill =>
            {
                skill.Property(s => s.Up).HasDefaultValue(1);
                skill.Property(s => s.UpgradeCost).HasDefaultValue(1000);
            });

        modelBuilder.Entity<User>().HasAlternateKey(x => x.Email);
    }
}

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime Birthday { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string NikName { get; set; }

    public long Score { get; set; }

    public long Money { get; set; }

    public Skill MoneySkill { get; set; }
    
    public Skill ScoreSkill { get; set; }

    public void UpgradeMoneySkill() => UpgradeSkill(MoneySkill ??= new());

    public void UpgradeScoreSkill() => UpgradeSkill(ScoreSkill ??= new());

    private void UpgradeSkill(Skill skill)
    {
        if (Money < skill.UpgradeCost)
            throw new InvalidOperationException($"Not enough banalce for upgrade. {nameof(Money)} - {Money}, Upgrade cost - {skill.UpgradeCost}");

        Money -= skill.UpgradeCost;
        skill.UpgradeCost += 1000;
        skill.Up += 1;
    }
}


[Owned]
public class Skill
{
    public float Up { get; set; }

    public long UpgradeCost { get; set; }
}
