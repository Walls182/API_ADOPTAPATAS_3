using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_ADOPTAPATAS_3.Repositories.Models;

public partial class BdadoptapatasContext : DbContext
{
    public BdadoptapatasContext()
    {
    }

    public BdadoptapatasContext(DbContextOptions<BdadoptapatasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adopcion> Adopcions { get; set; }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Canino> Caninos { get; set; }

    public virtual DbSet<Donacion> Donacions { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Fundacion> Fundacions { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adopcion>(entity =>
        {
            entity.HasKey(e => e.IdAdopcion).HasName("PK__Adopcion__210DF8ADA9F765B7");

            entity.ToTable("Adopcion");

            entity.Property(e => e.IdAdopcion).HasColumnName("idAdopcion");
            entity.Property(e => e.FechaAdopcion)
                .HasColumnType("date")
                .HasColumnName("fechaAdopcion");
            entity.Property(e => e.FkCanino).HasColumnName("fkCanino");
            entity.Property(e => e.FkUsuario).HasColumnName("fkUsuario");

            entity.HasOne(d => d.FkCaninoNavigation).WithMany(p => p.Adopcions)
                .HasForeignKey(d => d.FkCanino)
                .HasConstraintName("FK__Adopcion__fkCani__4CA06362");

            entity.HasOne(d => d.FkUsuarioNavigation).WithMany(p => p.Adopcions)
                .HasForeignKey(d => d.FkUsuario)
                .HasConstraintName("FK__Adopcion__fkUsua__4D94879B");
        });

        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("PK__Auditori__F1F307010EB89B65");

            entity.Property(e => e.IdAuditoria).HasColumnName("idAuditoria");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.NombreAuditor)
                .HasMaxLength(255)
                .HasColumnName("nombreAuditor");
        });

        modelBuilder.Entity<Canino>(entity =>
        {
            entity.HasKey(e => e.IdCanino).HasName("PK__Canino__605AE19B142FC62B");

            entity.ToTable("Canino");

            entity.Property(e => e.IdCanino).HasColumnName("idCanino");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Disponibilidad).HasColumnName("disponibilidad");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.EstadoSalud)
                .HasMaxLength(50)
                .HasColumnName("estadoSalud");
            entity.Property(e => e.FkEstado).HasColumnName("fkEstado");
            entity.Property(e => e.FkFundacion).HasColumnName("fkFundacion");
            entity.Property(e => e.Imagen).HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Raza)
                .HasMaxLength(50)
                .HasColumnName("raza");
            entity.Property(e => e.Temperamento)
                .HasMaxLength(50)
                .HasColumnName("temperamento");
            entity.Property(e => e.Vacunas).HasColumnName("vacunas");

            entity.HasOne(d => d.FkEstadoNavigation).WithMany(p => p.Caninos)
                .HasForeignKey(d => d.FkEstado)
                .HasConstraintName("FK__Canino__fkEstado__49C3F6B7");

            entity.HasOne(d => d.FkFundacionNavigation).WithMany(p => p.Caninos)
                .HasForeignKey(d => d.FkFundacion)
                .HasConstraintName("FK__Canino__fkFundac__48CFD27E");
        });

        modelBuilder.Entity<Donacion>(entity =>
        {
            entity.HasKey(e => e.IdDonacion).HasName("PK__Donacion__B260191733FEE7CB");

            entity.ToTable("Donacion");

            entity.Property(e => e.IdDonacion).HasColumnName("idDonacion");
            entity.Property(e => e.FechaDonacion)
                .HasColumnType("date")
                .HasColumnName("fechaDonacion");
            entity.Property(e => e.FkCanino).HasColumnName("fkCanino");
            entity.Property(e => e.FkUsuario).HasColumnName("fkUsuario");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.FkCaninoNavigation).WithMany(p => p.Donacions)
                .HasForeignKey(d => d.FkCanino)
                .HasConstraintName("FK__Donacion__fkCani__5070F446");

            entity.HasOne(d => d.FkUsuarioNavigation).WithMany(p => p.Donacions)
                .HasForeignKey(d => d.FkUsuario)
                .HasConstraintName("FK__Donacion__fkUsua__5165187F");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__62EA894ADFE48B58");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstado).HasColumnName("idEstado");
            entity.Property(e => e.DescripEstado)
                .HasMaxLength(255)
                .HasColumnName("descripEstado");
        });

        modelBuilder.Entity<Fundacion>(entity =>
        {
            entity.HasKey(e => e.IdFundacion).HasName("PK__Fundacio__70DC633BAF8E5903");

            entity.ToTable("Fundacion");

            entity.Property(e => e.IdFundacion).HasColumnName("idFundacion");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.Departamento)
                .HasMaxLength(255)
                .HasColumnName("departamento");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.FkEstado).HasColumnName("fkEstado");
            entity.Property(e => e.FkLogin).HasColumnName("fkLogin");
            entity.Property(e => e.FkRol).HasColumnName("fkRol");
            entity.Property(e => e.FotoFundacion).HasColumnName("fotoFundacion");
            entity.Property(e => e.LogoFundacion).HasColumnName("logoFundacion");
            entity.Property(e => e.Mision).HasColumnName("mision");
            entity.Property(e => e.Municipio)
                .HasMaxLength(255)
                .HasColumnName("municipio");
            entity.Property(e => e.NombreFundacion)
                .HasMaxLength(255)
                .HasColumnName("nombreFundacion");
            entity.Property(e => e.NombreRepresentante)
                .HasMaxLength(255)
                .HasColumnName("nombreRepresentante");
            entity.Property(e => e.ObjetivoSocial).HasColumnName("objetivoSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.Vision).HasColumnName("vision");

            entity.HasOne(d => d.FkEstadoNavigation).WithMany(p => p.Fundacions)
                .HasForeignKey(d => d.FkEstado)
                .HasConstraintName("FK__Fundacion__fkEst__45F365D3");

            entity.HasOne(d => d.FkLoginNavigation).WithMany(p => p.Fundacions)
                .HasForeignKey(d => d.FkLogin)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Fundacion__fkLog__440B1D61");

            entity.HasOne(d => d.FkRolNavigation).WithMany(p => p.Fundacions)
                .HasForeignKey(d => d.FkRol)
                .HasConstraintName("FK__Fundacion__fkRol__44FF419A");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.IdLogin).HasName("PK__Login__068B3EBB642B3F2A");

            entity.ToTable("Login");

            entity.Property(e => e.IdLogin).HasColumnName("idLogin");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .HasColumnName("usuario");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76FEE7AAB2");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A62B5C6E8C");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("correo");
            entity.Property(e => e.Departamento)
                .HasMaxLength(255)
                .HasColumnName("departamento");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.FkEstado).HasColumnName("fkEstado");
            entity.Property(e => e.FkLogin).HasColumnName("fkLogin");
            entity.Property(e => e.FkRol).HasColumnName("fkRol");
            entity.Property(e => e.Municipio)
                .HasMaxLength(255)
                .HasColumnName("municipio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.FkEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkEstado)
                .HasConstraintName("FK__Usuario__fkEstad__3F466844");

            entity.HasOne(d => d.FkLoginNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkLogin)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Usuario__fkLogin__3D5E1FD2");

            entity.HasOne(d => d.FkRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkRol)
                .HasConstraintName("FK__Usuario__fkRol__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
