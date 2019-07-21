using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Comentarios:
    ///     - id: id do comentário (int) 
    ///     - Texto : texto do comentário (string)
    ///     - EpisodioFk : chave forasteira para a tabela Episodios (int)
    ///     - UtilizadorFK : chave forasteira para a tabela Utilizadores (int)
    /// </summary>
    public class Comentarios
    {
        /// <summary>
        /// id: id do comentário (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Texto : texto do comentário (string)
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// EpisodioFk : chave forasteira para a tabela Episodios (int)
        /// </summary>
        [ForeignKey("Episodio")]
        public int EpisodioFK { get; set; }
        public virtual Episodios Episodio { get; set; }

        /// <summary>
        /// UtilizadorFK : chave forasteira para a tabela Utilizadores (int)
        /// </summary>
        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }
    }
}

 

