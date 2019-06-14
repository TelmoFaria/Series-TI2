using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    //teste de atualizaçao
    public class Series
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Genero { get; set; }


        public string Foto { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public string Sinopse { get; set; }

        public string Video { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public double Classificacao { get; set; }


        public virtual ICollection<Temporadas> Temporadas { get; set; }

        [ForeignKey("Editora")]
        public int EditoraFK { get; set; }
        public virtual Editora Editora { get; set; }



    }
}