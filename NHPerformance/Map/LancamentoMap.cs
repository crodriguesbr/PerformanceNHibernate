using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHPerformance
{
    public class LancamentoMap : ClassMapping<Lancamento>
    {
        public LancamentoMap()
        {
            Id(x => x.Id, a =>
            {
                a.Generator(Generators.Identity);
            });
            Property(x => x.Data, a =>
            {
                a.NotNullable(true);
            });
            Property(x => x.Valor, a =>
            {
                a.NotNullable(true);
            });
            ManyToOne(x => x.Conta, map =>
            {
                map.Column("IdConta");
            });
        }
    }   
}
