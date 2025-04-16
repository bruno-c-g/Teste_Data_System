using System;
using TesteDataSystem.Domain.Enums;
using TesteDataSystem.Domain.Validation;

namespace TesteDataSystem.Domain.Entities
{
    public class DataBase
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
        public DateTime? DataConclusao { get; private set; }
        public StatusEnum Status { get; private set; } = StatusEnum.Pendente;

        public DataBase(int id,
                        string titulo,
                        string descricao,
                        DateTime dataCriacao,
                        DateTime? dataConclusao,
                        StatusEnum status)
        {
            DomainExceptionValidation.When(id < 0, "O Id deve ser positivo!");
            Id = id;
            ValidateDomain(titulo,
                           descricao,
                           dataCriacao,
                           dataConclusao,
                           status);
        }

        public DataBase(string titulo, 
                        string descricao, 
                        DateTime dataCriacao, 
                        DateTime? dataConclusao, 
                        StatusEnum status)
        {
            ValidateDomain(titulo,
                           descricao,
                           dataCriacao,
                           dataConclusao,
                           status);
        }

        public void Update(string titulo,
                           string descricao,
                           DateTime dataCriacao,
                           DateTime? dataConclusao,
                           StatusEnum status)
        {
            ValidateDomain(titulo,
                           descricao,
                           dataCriacao,
                           dataConclusao,
                           status);
        }

        public void ValidateDomain(string titulo,
                        string descricao,
                        DateTime dataCriacao,
                        DateTime? dataConclusao,
                        StatusEnum status)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(titulo), "O campo título é obrigatório!");
            DomainExceptionValidation.When(titulo.Length > 100, "O campo título deve ter no máximo 100 caracteres!");
            DomainExceptionValidation.When(dataConclusao.HasValue && dataConclusao.Value < dataCriacao, 
                                           "A data de conclusão não pode ser anterior à data de criação!");
            Titulo = titulo;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            Status = status;
        }
    }
}
