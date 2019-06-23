using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class Temporadas
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        [DisplayName("Nome Temporada")]
        public string Nome { get; set; }


        public string Foto { get; set; }

        public string Trailer { get; set; }

        [ForeignKey("Series")]
        public int SerieFK { get; set; }
        public virtual Series Series { get; set; }

        public virtual ICollection<Episodios> Episodios { get; set; }

    }
}