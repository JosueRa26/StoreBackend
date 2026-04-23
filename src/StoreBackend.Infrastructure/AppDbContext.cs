using Microsoft.EntityFrameworkCore;
using System;
using StoreBackend.Domain.Entities;
using System.Net.Http.Headers;



namespace StoreBackend.Infrastructure;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<User> Users { get; set; }



}