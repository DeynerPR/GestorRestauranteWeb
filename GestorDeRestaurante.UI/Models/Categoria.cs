using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeRestaurante.Models
{
    public enum Categoria
    {
        Entrada = 1,

        [Display(Name = "Pequeñas Botanas")]
        PequeñasBotanas = 2,

        Aperitivos = 3,

        [Display(Name = "Sopas y Ensaladas")]
        SopasYEnsaladas = 4,

        [Display(Name = "Platos Principales")]
        PlatosPrincipales = 5,

        Postres = 6,

        Bebidas = 7
    }
}
