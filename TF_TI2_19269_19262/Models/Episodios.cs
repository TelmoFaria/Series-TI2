using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class Episodios
    {
        [Key]
        public int ID { get; set; }


        public int Numero { get; set; }

        public string Nome { get; set; }


        public string Sinopse { get; set; }

        public string Foto { get; set; }

        public string Trailer { get; set; }

        [NotMapped] // atributo nao aparece na BD
        [RegularExpression("([0-9](,[0-9])?|10)", ErrorMessage ="escrever msg correta")]
        public string AuxClassificacao { get; set; }

        public double Classificacao { get; set; }

        [ForeignKey("Temporadas")]
        public int TemporadaFK { get; set; }
        public virtual Temporadas Temporadas { get; set; }

        public virtual ICollection<Comentarios> ListaDeComentarios { get; set; }

        public virtual ICollection<PessoasEpisodios> PessoasEpisodios { get; set; }
    }
}

/*
    Tabela Episodios
            - ID : id do episódio (int)
            - Numero : numero do episódio (int)
            - Nome : nome do episódio (string)
            - Sinopse : sinopse/descirção do episódio (string)
            - Foto : fotografia/imagem do episódio (string)
            - Trailer : trailer do episódio (string)
            - AuxClassificacao : variavel auxiliar para introdução da classificação na bd (string)
            - Classificacao : Classificação do episódio (double)
            - TemporadaFK : chave forasteira para a tabela Temporadas (int)
            - ListaDeComentarios : lista de comentários (ICollection)
            - PessoasEpisodios : lista de papeis (ICollection)
*/
