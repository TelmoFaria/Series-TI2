﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TF_TI2_19269_19262.Models
{
    public class Comentarios
    {
        [Key]
        public int ID { get; set; }


        public string Texto { get; set; }

        [ForeignKey("Episodio")]
        public int EpisodioFK { get; set; }
        public virtual Episodios Episodio { get; set; }

        [ForeignKey("Utilizador")]
        public int UtilizadorFK { get; set; }
        public virtual Utilizadores Utilizador { get; set; }
    }
}

/*
 Tabela Comentarios:
        - id: id do comentário (int)
        - Texto : texto do comentário (string)
        - EpisodioFk : chave forasteira para a tabela Episodios (int)
        - UtilizadorFK : chave forasteira para a tabela Utilizadores (int)
*/
