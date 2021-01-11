using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Cinema.Models
{
    public class Recenzie
    {
        public int RecenzieId { get; set; }
        [MinLength(2, ErrorMessage = "Title cannot be less than 2!")]
        public string Titlu { get; set; }
        public string Descriere { get; set; }
        public int Nota { get; set; }
        public string NumeUtilizator { get; set; }
        public Film Film { get; set; }

    }
}