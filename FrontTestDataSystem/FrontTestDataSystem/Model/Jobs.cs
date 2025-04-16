using FrontTestDataSystem.Enums;
using System;

namespace FrontTestDataSystem.Model
{
    public class Jobs
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusEnum Status { get; set; }
    }
}
