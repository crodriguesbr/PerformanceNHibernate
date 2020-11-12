using System;
using System.Collections.Generic;
using System.Linq;

namespace NHPerformance
{
    public class Conta
    {
        private readonly ICollection<Lancamento> _lancamentos;

        public virtual long Id { get; set; }
        public virtual long Numero { get; set; }
        public virtual Cliente Cliente { get; set; }


        public Conta()
        {
            _lancamentos = new HashSet<Lancamento>();
        }

        public virtual void Debitar(decimal valor)
        {
            _lancamentos.Add(new Lancamento() { Conta = this, Data = DateTime.Now, Valor = valor });
        }

        public virtual void Creditar(decimal valor)
        {
            _lancamentos.Add(new Lancamento() { Conta = this, Data = DateTime.Now, Valor = valor });
        }

        public virtual int QuantidadeLancamentos()
        {
            return _lancamentos.Count();
        }

    }
}
