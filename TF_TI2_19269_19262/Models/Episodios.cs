using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class Episodios
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public string Sinopse { get; set; }

        public string Foto { get; set; }

        public string Trailer { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public double Classificacao { get; set; }

        [ForeignKey("Temporadas")]
        public int TemporadaFK { get; set; }
        public virtual Temporadas Temporadas { get; set; }

        public virtual ICollection<Comentarios> Comentarios { get; set; }

        public virtual ICollection<PessoasEpisodios> PessoasEpisodios { get; set; }
    }
}