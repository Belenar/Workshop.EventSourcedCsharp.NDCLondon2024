using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Read_database;

public class Read_context : DbContext
{
    public Read_context(DbContextOptions<Read_context> options) : base(options)
    { }

    public DbSet<Box_status> Box_statuses 
        => Set<Box_status>();
    public DbSet<Projection_checkpoint> Projection_checkpoints 
        => Set<Projection_checkpoint>();
}

public class Box_status
{
    // Lazy
    [Key]
    public Guid Aggregate_id { get; set; }
    public int Number_of_bottles { get; set; }
    public Shipment_status Shipment_status { get; set; }
}

public enum Shipment_status
{
    Open,
    Closed,
    Shipped
}

public class Projection_checkpoint
{
    // Still lazy
    [Key]
    [MaxLength(256)]
    public string Projection_name { get; set; }
    public ulong Event_version { get; set; }
}