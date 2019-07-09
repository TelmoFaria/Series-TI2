using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName ("Nome Série")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Genero { get; set; }

        public string Foto { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public string Sinopse { get; set; }

        public string Video { get; set; }

        [NotMapped]
        [RegularExpression ("[0-9](,[0-9])?|10",ErrorMessage =("A Classificação que introduziu não é válida"))]
        public string AuxClassificacao { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public double Classificacao { get; set; }

        public virtual ICollection<Temporadas> Temporadas { get; set; }

        [ForeignKey("Editora")]
        [DisplayName ("Editora")]
        public int EditoraFK { get; set; }
        public virtual Editora Editora { get; set; }
    }
}

/*
    Tabela Series
            - Id : id da série (int)
            - Nome : nome da série (string)
            - Genero : genero da série (string)
            - Foto : fotografia/imagem da série (string)
            - Sinopse : sinopse/descriçao da série (string)
            - Video : trailler da série (string)
            - AuxClassificacao : variavel auxiliar para introdução da classificação na bd (string)
            - Classificacao : classificação da série (double)
            - EditoraFK : chave forasteira para a tabela Editora (int)
*/
