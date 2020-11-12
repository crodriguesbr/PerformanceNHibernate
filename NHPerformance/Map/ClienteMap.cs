using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHPerformance
{
    public class ClienteMap : ClassMapping<Cliente>
    {
        public ClienteMap()
        {
            Cache(x =>
            {
                x.Usage(CacheUsage.NonstrictReadWrite);
                x.Region("cliente");
            });

            Id(x => x.Id, a =>
            {
                a.Generator(Generators.Identity);
            });
            Property(x => x.Nome, a =>
            {
                a.NotNullable(true);
                a.Length(200);
            });
            Property(x => x.DataCadastro, a =>
            {
                a.NotNullable(true);
            });
        }
    }
}
