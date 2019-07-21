using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Series:
    ///     - ID : id da série (int)
    ///     - Nome : nome da série (string)
    ///     - Genero : genero da série (string)
    ///     - Foto : fotografia/imagem da série (string)
    ///     - Sinopse : sinopse/descriçao da série (string)
    ///     - Video : trailler da série (string)
    ///     - AuxClassificacao : variavel auxiliar para introdução da classificação na bd (string)
    ///     - Classificacao : classificação da série (double)
    ///     - EditoraFK : chave forasteira para a tabela Editora (int)
    /// </summary>
    public class Series
    {
        public Series()
        {
            Temporadas = new HashSet<Temporadas>();
        }
        /// <summary>
        /// ID : id da série (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nome : nome da série (string)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        [DisplayName ("Nome Série")]
        public string Nome { get; set; }

        /// <summary>
        /// Genero : genero da série (string)
        /// </summary>
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Genero { get; set; }

        /// <summary>
        /// Foto : fotografia/imagem da série (string)
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Sinopse : sinopse/descriçao da série (string)
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public string Sinopse { get; set; }

        /// <summary>
        /// Video : trailler da série (string)
        /// </summary>
        public string Video { get; set; }

        /// <summary>
        /// AuxClassificacao : variavel auxiliar para introdução da classificação na bd (string)
        /// </summary>
        [NotMapped]
        [RegularExpression ("[0-9](,[0-9])?|10",ErrorMessage =("A Classificação que introduziu não é válida"))]
        public string AuxClassificacao { get; set; }

        /// <summary>
        /// Classificacao : classificação da série (double)
        /// </summary>
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!")]
        public double Classificacao { get; set; }

        /// <summary>
        /// Temporadas : Lista de Temporadas
        /// </summary>
        public virtual ICollection<Temporadas> Temporadas { get; set; }

        /// <summary>
        /// EditoraFK : chave forasteira para a tabela Editora (int)        
        /// /// </summary>
        [ForeignKey("Editora")]
        [DisplayName ("Editora")]
        public int EditoraFK { get; set; }
        public virtual Editora Editora { get; set; }
    }
}
