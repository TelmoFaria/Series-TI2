using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Editora
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Logo { get; set; }

        public virtual ICollection<Series> Series { get; set; }
    }
}

/*
    Tabela Editora:
            - ID : id da editora (int)
            - nome : nome da editora (string)
            - logo : logotipo/imagem da editora (string)
            - Series : lista de séries (ICollection)
*/
