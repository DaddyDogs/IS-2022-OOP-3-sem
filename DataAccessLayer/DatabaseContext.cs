using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public sealed class DatabaseContext : DbContext
{
#pragma warning disable CS8618
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
#pragma warning restore CS8618
    public DbSet<PrivateAccount> PrivateAccounts { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
    public DbSet<Messenger> Messengers { get; set; }
    public DbSet<MessengerMessage> MessengerMessages { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PhoneMessage> PhoneMessages { get; set; }
    public DbSet<Subordinate> Subordinates { get; set; }
    public DbSet<Supervisor> Supervisors { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<MessageSource> MessageSource { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<TemporaryId> TemporaryIds { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<MessageSourceIdentifier> MessageSourceIdentifiers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageSourceIdentifier>().ToTable("Messages sources identifiers");
        modelBuilder.Entity<TemporaryId>().ToTable("TempId");

        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Employee>().Property(t => t.Identifier).HasColumnName("Identifier");
        modelBuilder.Entity<Employee>(builder =>
        {
            builder.HasOne(x => x.PrivateAccount).WithOne(y => y.Employee).HasForeignKey<PrivateAccount>(a => a.Id);
        });

        modelBuilder.Entity<Supervisor>(builder =>
        {
            builder.HasMany(x => x.Employees);
        });

        modelBuilder.Entity<PrivateAccount>(builder =>
        {
            builder.HasMany(x => x.Accounts);
        });

        modelBuilder.Entity<MessageSource>(builder =>
        {
            builder.HasMany(x => x.Messages);
        });
        modelBuilder.Entity<Account>(builder =>
        {
            builder.HasOne(x => x.MessageSource);
            builder.HasMany(x => x.Addressees);
        });
        modelBuilder.Entity<MessageSource>(builder =>
        {
            builder.HasMany(x => x.Messages);
        });
    }
}