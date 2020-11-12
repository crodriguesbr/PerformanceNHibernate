using System;

namespace NHPerformance
{
    public class Lancamento
    {
        public virtual long Id { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Conta Conta { get; set; }

        public Lancamento()
        {
            
        }


    }
}
