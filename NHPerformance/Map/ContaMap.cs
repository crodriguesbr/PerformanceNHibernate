using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHPerformance
{
    public class ContaMap : ClassMapping<Conta>
    {
        public ContaMap()
        {
            Id(x => x.Id, a =>
            {
                a.Generator(Generators.Identity);
            });
            Property(x => x.Numero, a =>
            {
                a.NotNullable(true);
                a.Length(200);
            });

            ManyToOne(a => a.Cliente, a =>
            {
                a.Column("IdCliente");
                //a.Lazy(LazyRelation.NoLazy);
                //a.Fetch(FetchKind.Join);
            });

            Bag<Lancamento>("_lancamentos", bag => {
                bag.Key(k => {
                    k.Column(col => col.Name("IdConta"));
                    k.NotNullable(true);
                });
                bag.Inverse(true);
                bag.Access(Accessor.ReadOnly);
                bag.Cascade(Cascade.All | Cascade.DeleteOrphans);
                bag.BatchSize(10);
                bag.Lazy(CollectionLazy.Extra);
            }, a => a.OneToMany());
        }
    }
}
