using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    /// <summary>
    /// Tabela Episodios
    ///     - ID : id do episódio (int)
    ///     - Numero : numero do episódio (int)
    ///     - Nome : nome do episódio (string)
    ///     - Sinopse : sinopse/descirção do episódio (string)
    ///     - Foto : fotografia/imagem do episódio (string)
    ///     - Trailer : trailer do episódio (string)
    ///     - AuxClassificacao : variavel auxiliar para introdução da classificação na bd (string)
    ///     - Classificacao : Classificação do episódio (double)
    ///     - TemporadaFK : chave forasteira para a tabela Temporadas (int)
    ///     - ListaDeComentarios : lista de comentários (ICollection)
    ///     - PessoasEpisodios : lista de papeis (ICollection)           
    /// </summary>
    public class Episodios
    {
        public Episodios()
        {
            ListaDeComentarios = new HashSet<Comentarios>();
            PessoasEpisodios = new HashSet<PessoasEpisodios>();
        }
        /// <summary>
        /// ID : id do episódio (int)
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Numero : numero do episódio (int)
        /// </summary>
        [Required]
        public int Numero { get; set; }

        /// <summary>
        /// Nome : nome do episódio (string)
        /// </summary>
        [Required]
        //[RegularExpression("[A-Z]", ErrorMessage = "O nome tem de começar com letra maiúscula")]
        public string Nome { get; set; }

        /// <summary>
        /// Sinopse : sinopse/descirção do episódio (string)
        /// </summary>
        //[RegularExpression("[A-Z]", ErrorMessage = "A sinopse tem de começar com letra maiúscula")]
        public string Sinopse { get; set; }

        /// <summary>
        /// Foto : fotografia/imagem do episódio (string)
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Trailer : trailer do episódio (string)
        /// </summary>
        public string Trailer { get; set; }

        /// <summary>
        /// AuxClassificacao : variavel auxiliar para introdução da classificação na bd (string)
        /// </summary>
        [NotMapped] // atributo nao aparece na BD
        [RegularExpression("([0-9](,[0-9])?|10)", ErrorMessage ="escrever msg correta")]
        public string AuxClassificacao { get; set; }

        /// <summary>
        /// Classificacao : Classificação do episódio (double)
        /// </summary>
        public double Classificacao { get; set; }

        /// <summary>
        /// TemporadaFK : chave forasteira para a tabela Temporadas (int)
        /// </summary>
        [ForeignKey("Temporada")]
        public int TemporadaFK { get; set; }
        public virtual Temporadas Temporada { get; set; }

        /// <summary>
        /// ListaDeComentarios : lista de comentários (ICollection)
        /// </summary>
        public virtual ICollection<Comentarios> ListaDeComentarios { get; set; }

        /// <summary>
        /// PessoasEpisodios : lista de papeis (ICollection)
        /// </summary>
        public virtual ICollection<PessoasEpisodios> PessoasEpisodios { get; set; }
    }
}


    
