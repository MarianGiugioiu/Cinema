using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }
        public DbSet<Recenzie> Recenzii { get; set; }
        public DbSet<Film> Filme { get; set; }

        public class Initp : DropCreateDatabaseIfModelChanges<DbCtx>
        { // custom initializer
            protected override void Seed(DbCtx ctx)
            {
                Film film = new Film { Denumire = "film" };
                ctx.Filme.Add(film);
                ctx.Recenzii.Add(new Recenzie {Titlu = "Titlu1", Descriere="Descriere1", Nota=2, NumeUtilizator="Utilizator1", Film=film  });
                ctx.Recenzii.Add(new Recenzie { Titlu = "Titlu2", Descriere = "Descriere2", Nota = 3, NumeUtilizator = "Utilizator2", Film = film });
                ctx.SaveChanges();
                base.Seed(ctx);
            }
        }


    }
}