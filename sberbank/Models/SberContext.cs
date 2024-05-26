using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sberbank.Models;

public partial class SberContext : DbContext
{
    public SberContext()
    {
    }

    public SberContext(DbContextOptions<SberContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Deposit> Deposits { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-NSNU3CD4\\SQLEXPRESS;Database=sber;Trusted_Connection=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount).HasName("PK__Account__213379EB89BBBACA");

            entity.ToTable("Account");

            entity.Property(e => e.IdAccount).HasColumnName("ID_Account");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.ClosingDate).HasColumnName("closing_date");
            entity.Property(e => e.DepositAmount)
                .HasColumnType("money")
                .HasColumnName("deposit_amount");
            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
            entity.Property(e => e.IdDeposit).HasColumnName("ID_Deposit");
            entity.Property(e => e.OpeningDate).HasColumnName("opening_date");
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasColumnName("status");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Client");

            entity.HasOne(d => d.IdDepositNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IdDeposit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Deposit");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__Client__B5AE4EC84F88637E");

            entity.ToTable("Client");

            entity.HasIndex(e => e.Passport, "UQ__Client__5E2A085771FAFE0E").IsUnique();

            entity.HasIndex(e => e.Login, "UQ__Client__7838F272413613C6").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Client__AB6E6164624DE4B5").IsUnique();

            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirthday).HasColumnName("date_of_birthday");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasMaxLength(100)
                .HasColumnName("fname");
            entity.Property(e => e.House)
                .HasMaxLength(10)
                .HasColumnName("house");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(1)
                .HasColumnName("is_active");
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .HasColumnName("lname");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Passport)
                .HasMaxLength(20)
                .HasColumnName("passport");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Room)
                .HasMaxLength(10)
                .HasColumnName("room");
            entity.Property(e => e.Sname)
                .HasMaxLength(100)
                .HasColumnName("sname");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.HasKey(e => e.IdDeposit).HasName("PK__Deposit__08E6B59BA53733F8");

            entity.ToTable("Deposit");

            entity.Property(e => e.IdDeposit).HasColumnName("ID_Deposit");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");
            entity.Property(e => e.DepositTerm).HasColumnName("deposit_term");
            entity.Property(e => e.InterestPeriod).HasColumnName("interest_period");
            entity.Property(e => e.InterestRate)
                .HasColumnType("money")
                .HasColumnName("interest_rate");
            entity.Property(e => e.IsActivity).HasColumnName("is_activity");
            entity.Property(e => e.MinimumDeposit)
                .HasColumnType("money")
                .HasColumnName("minimum_deposit");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PossibilityOfRemoval)
                .HasDefaultValue(1)
                .HasColumnName("possibility_of_removal");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.IdOperation).HasName("PK__Operatio__C626DC5279425621");

            entity.ToTable("Operation");

            entity.Property(e => e.IdOperation).HasColumnName("ID_Operation");
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IdAccount).HasColumnName("ID_Account");
            entity.Property(e => e.IdClient).HasColumnName("ID_Client");
            entity.Property(e => e.Sum)
                .HasColumnType("money")
                .HasColumnName("sum");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.IdAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_Account");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Operations)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operation_Client");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
