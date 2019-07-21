using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela PessoasEpisodios (papeis)
    ///     - Id : id do papel (int)
    ///     - tipoDePapel : uma pessoa ser ator ou realizador (enumerable)
    ///     - papel : representa 1 papel 
    ///     - PessoaFK : chave forasteira para a tabela Pessoas (int)
    ///     - EpisodioFk : chave forasteira para a tabela Episodios (int)
    /// </summary>
    public class PessoasEpisodios
    {
        /// <summary>
        /// ID : id do papel (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// tipoDePapel : uma pessoa ser ator ou realizador (enumerable)
        /// </summary>
        public enum TipoDePapel
        {
            Ator,
            Realizador,
        };

        /// <summary>
        /// papel : representa 1 papel 
        /// </summary>
        public TipoDePapel Papel {get; set;}

        /// <summary>
        /// PessoaFK : chave forasteira para a tabela Pessoas (int)
        /// </summary>
        [ForeignKey("Pessoa")]
        public int PessoaFK { get; set; }
        public virtual Pessoas Pessoa { get; set; }

        /// <summary>
        /// EpisodioFk : chave forasteira para a tabela Episodios (int)
        /// </summary>
        [ForeignKey("Episodio")]
        public int EpisodioFK { get; set; }
        public virtual Episodios Episodio { get; set; }
    }
}
