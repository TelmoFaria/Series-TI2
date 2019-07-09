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

        [ForeignKey("Serie")]
        public int SerieFK { get; set; }
        public virtual Series Serie { get; set; }

        public virtual ICollection<Episodios> Episodios { get; set; }

    }
}

/*
    Tabela Temporadas
            - Id : id da temporada (int)
            - Numero : numeor da temporada (int)
            - Nome : nome da temporada (string)
            - Foto : fotografia/imagem da temporada (string)
            - Trailer : trailler da temporada (string)
            - SeriesFK : chave forasteira para a tabela Series (int)
            - Episodios : lista de Episódios  (ICollection)
*/
