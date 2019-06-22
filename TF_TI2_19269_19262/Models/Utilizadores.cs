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



       // [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!!")]
      //  [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüçãõ]+(( |'|-| dos | da | de | e | d')[A-ZÍÉ][a-záéíóúàèìòùâêîôûäëïöüçãõ]+){1,3}"
      //  , ErrorMessage = "o {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula seguida de minúsculas...")]
        public string Nome { get; set; }



       // [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!!")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        
        public virtual ICollection<Comentarios> ListaDeComentarios { get; set; }
    }
}