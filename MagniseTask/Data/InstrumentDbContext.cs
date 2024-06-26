namespace MagniseTask.Data;

using MagniseTask.Data.Helpers.Json;
using MagniseTask.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

public class InstrumentDbContext : DbContext
{
    public DbSet<Instrument> Instruments { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.
           UseInMemoryDatabase ("RectangleDatabase").
           LogTo (Console.WriteLine);
    }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrument> (x =>
        {
            x.HasKey (e => e.Id);
            x.Property (e => e.Id).ValueGeneratedNever ();
        });

        var instruments = Seed ();
        modelBuilder.Entity<Instrument> ().HasData (instruments);
    }

    public InstrumentDbContext () : base ()
    {
        Database.EnsureCreated ();
    }

    private static List<Instrument> Seed ()
    {
        string instrumentsInJson = File.ReadAllText ("AllInstruments.json");
        var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>> (instrumentsInJson);
        var instruments = new List<Instrument> ();
        if ( jsonData is not null && jsonData.TryGetValue ("data", out var value) )
        {
            instruments = JsonSerializer.Deserialize<List<InstrumentJsonDto>> (( JsonElement ) value).Select (x => new Instrument (
                x.id, x.symbol, x.kind, x.description, x.tickSize, x.currency, x.baseCurrency)).ToList ();
        }
        return instruments;
    }
}