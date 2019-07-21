using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Editora:
    ///     - ID : id da editora (int)
    ///     - Nome : nome da editora (string)
    ///     - Logo : logotipo/imagem da editora (string)
    ///     - Series : lista de séries (ICollection)
    /// </summary>
    public class Editora
    {
        public Editora()
        {
            Series = new HashSet<Series>();
        }
        /// <summary>
        /// ID : id da editora (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nome : nome da editora (string)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        /// <summary>
        /// Logo : logotipo/imagem da editora (string)
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Series : lista de séries (ICollection)
        /// </summary>
        public virtual ICollection<Series> Series { get; set; }
    }
}

    

