using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Temporadas:
    ///     - ID : id da temporada (int)
    ///     - Numero : numeor da temporada (int)
    ///     - Nome : nome da temporada (string)
    ///     - Foto : fotografia/imagem da temporada (string)
    ///     - Trailer : trailler da temporada (string)
    ///     - SeriesFK : chave forasteira para a tabela Series (int)
    ///     - Episodios : lista de Episódios  (ICollection)
    /// </summary>
    public class Temporadas
    {
        public Temporadas()
        {
            Episodios = new HashSet<Episodios>();
        }
        /// <summary>
        /// ID : id da temporada (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Numero : numeor da temporada (int)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public int Numero { get; set; }

        /// <summary>
        /// Nome : nome da temporada (string)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        [DisplayName("Nome Temporada")]
        public string Nome { get; set; }

        /// <summary>
        /// Foto : fotografia/imagem da temporada (string)
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Trailer : trailler da temporada (string)
        /// </summary>
        public string Trailer { get; set; }

        /// <summary>
        /// SeriesFK : chave forasteira para a tabela Series (int)
        /// </summary>
        [ForeignKey("Serie")]
        public int SerieFK { get; set; }
        public virtual Series Serie { get; set; }

        /// <summary>
        /// Episodios : lista de Episódios  (ICollection)
        /// </summary>
        public virtual ICollection<Episodios> Episodios { get; set; }
    }
}
