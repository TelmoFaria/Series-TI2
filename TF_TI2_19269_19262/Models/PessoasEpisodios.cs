using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class PessoasEpisodios
    {
        [Key]
        public int ID { get; set; }




        public enum TipoDePapel
        {
            Ator,
            Realizador,
        };

        public TipoDePapel Papel {get; set;}

        [ForeignKey("Pessoas")]
        public int PessoaFK { get; set; }
        public virtual Pessoas Pessoas { get; set; }

        [ForeignKey("Episodios")]
        public int EpisodioFK { get; set; }
        public virtual Episodios Episodios { get; set; }



    }
}