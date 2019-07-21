using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Utilizadores:
    ///     - ID : id do utilizador (int)
    ///     - Email : e-mail do utilizador (string)
    ///     - Nome : nome do utilizador (string)
    ///     - UserName : nick name do utilizador (string)
    /// </summary>
    public class Utilizadores
    {
        public Utilizadores()
        {
            ListaDeComentarios = new HashSet<Comentarios>();
        }

        /// <summary>
        /// ID : id do utilizador (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Email : e-mail do utilizador (string)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Nome : nome do utilizador (string)
        /// </summary>
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        /// <summary>
        /// UserName : nick name do utilizador (string)
        /// </summary>
        public virtual ICollection<Comentarios> ListaDeComentarios { get; set; }
    }
}
