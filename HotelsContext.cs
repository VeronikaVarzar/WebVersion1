using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryWebApplication
{
    public partial class HotelsContext : DbContext
    {
        public HotelsContext()
        {
        }

        public HotelsContext(DbContextOptions<HotelsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clientage { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Parking> Parking { get; set; }
        public virtual DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<RoomForRent> RoomsForRent { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-173RIGH; Database=Hotels; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PassportId)
                    .IsRequired()
                    .HasColumnName("PassportID");

                entity.Property(e => e.Surname).IsRequired();
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Number).IsRequired();

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.HotelRooms)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelRooms_State");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.HotelRooms)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelRooms_RoomTypes");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.TownId).HasColumnName("TownID");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.TownId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotels_Towns");
            });

            modelBuilder.Entity<Parking>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Number).IsRequired();

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Parking)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parking_Hotels");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Parking)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parking_State");
            });

            modelBuilder.Entity<ParkingSpace>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckInDate).HasColumnType("date");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.DateOfEviction).HasColumnType("date");

                entity.Property(e => e.ParkingId).HasColumnName("ParkingID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ParkingSpaces)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParkingSpaces_Clientage");

                entity.HasOne(d => d.Parking)
                    .WithMany(p => p.ParkingSpaces)
                    .HasForeignKey(d => d.ParkingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParkingSpaces_ParkingSpaces");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.HotelId).HasColumnName("HotelID");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.RoomTypes)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomTypes_Hotels");
            });

            modelBuilder.Entity<RoomForRent>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CheckInDate).HasColumnType("date");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.DateOfEviction).HasColumnType("date");

                entity.Property(e => e.HotelRoomId).HasColumnName("HotelRoomID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RoomsForRent)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomsForRent_Clientage");

                entity.HasOne(d => d.HotelRoom)
                    .WithMany(p => p.RoomsForRent)
                    .HasForeignKey(d => d.HotelRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomsForRent_HotelRooms");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Towns)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Towns_Countries");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
