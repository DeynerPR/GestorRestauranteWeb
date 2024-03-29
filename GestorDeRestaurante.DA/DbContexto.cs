﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.DA
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> opciones) : base(opciones) { }
        
        

        public DbSet<GestorDeRestaurante.Model.Ingrediente> Ingredientes { get; set; }
        public DbSet<GestorDeRestaurante.Model.Medida> Medidas { get; set; }
        public DbSet<GestorDeRestaurante.Model.Platillo> Menu { get; set; }
        public DbSet<GestorDeRestaurante.Model.MenuIngrediente> MenuIngredientes { get; set; }
        public DbSet<GestorDeRestaurante.Model.Mesa> Mesas { get; set; }
        public DbSet<GestorDeRestaurante.Model.Orden> Ordens { get; set; }





    }
}
