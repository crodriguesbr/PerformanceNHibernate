using System;
namespace NHPerformance
{
    public class Cliente
    {
        public virtual long Id { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual DateTime DataCadastro { get; protected set; }

        public Cliente()
        {
            DataCadastro = DateTime.Now;
        }

        public Cliente(string nome)
            : this()
        {
            Nome = nome;
        }
    }
}
