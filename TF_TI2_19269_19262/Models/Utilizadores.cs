using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class Utilizadores
    {
        public Utilizadores()
        {
            ListaDeComentarios = new HashSet<Comentarios>();

        }

        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Nome { get; set; }

        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        
        public virtual ICollection<Comentarios> ListaDeComentarios { get; set; }
    }
}

/*
       Tabela Utilizadores:
                - Id : id do utilizador (int)
                - Email : e-mail do utilizador (string)
                - nome : nome do utilizador (string)
                - userName : nick name do utilizador (string)
*/
