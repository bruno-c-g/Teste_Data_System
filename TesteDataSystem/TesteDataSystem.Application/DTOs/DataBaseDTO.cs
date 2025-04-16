using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using TesteDataSystem.Domain.Enums;

namespace TesteDataSystem.Application.DTOs
{
    public class DataBaseDTO
    {
        private int _id;

        [IgnoreDataMember]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Título deve possuir no máximo 100 caracteres.")]
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusEnum Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DataConclusao.HasValue && DataConclusao <= DataCriacao)
            {
                yield return new ValidationResult(
                    "A data de conclusão deve ser posterior à data de criação.",
                    new[] { nameof(DataConclusao) });
            }
        }
    }
}
