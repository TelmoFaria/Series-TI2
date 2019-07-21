using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Pessoas: 
    ///     - ID : id da pessoa (int)
    ///     - Nome : nome da pessoa (string)
    ///     - Foto : fotografia/imagem da pessoa (string)
    ///     - PessoasEpisodios : lista de papeis (ICollection)
    /// </summary>
    public class Pessoas
    {
        public Pessoas()
        {
            PessoasEpisodios = new HashSet<PessoasEpisodios>();
        }
        /// <summary>
        /// ID : id da pessoa (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nome : nome da pessoa (string)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        /// <summary>
        /// Foto : fotografia/imagem da pessoa (string)
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// PessoasEpisodios : lista de papeis (ICollection)
        /// </summary>
        public virtual ICollection<PessoasEpisodios> PessoasEpisodios { get; set; }
    }
}

