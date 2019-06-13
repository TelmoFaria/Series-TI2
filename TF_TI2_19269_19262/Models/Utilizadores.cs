using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class Utilizadores
    {

        public int ID { get; set; }

        public string Nome { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Nacionalidade { get; set; }

        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}