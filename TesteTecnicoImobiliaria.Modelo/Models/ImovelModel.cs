﻿using Dapper.Contrib.Extensions;

namespace TesteTecnicoImobiliaria.Modelo.Models
{
    [Table("IMOVEL")]
    public class ImovelModel
    {
        [Key]
        public int CD_IMOVEL { get; set; }
        public int CD_CLIENTE { get; set; }
        public int CD_TIPO_IMOVEL { get; set; }
        public decimal VL_IMOVEL { get; set; }
        public DateTime DT_PUBLICACAO { get; set; }
        public string DS_IMOVEL { get; set; }
        public bool FL_ATIVO { get; set; }
    }
}
