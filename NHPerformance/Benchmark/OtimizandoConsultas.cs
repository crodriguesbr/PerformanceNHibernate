using System;
using NHibernate.Transform;

namespace NHPerformance
{
    public class OtimizandoConsultas: SessionFactoryBuild
    {
        public OtimizandoConsultas()
        {
        }

        public void UsandoDto()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            var conta = session.Get<Conta>((long)1);

            var conta1 = session.QueryOver<Conta>()
                        .JoinQueryOver<Cliente>(c => c.Cliente)
                        .Where(cli => cli.Id == 12)
                        .SingleOrDefault();


            Cliente clienteAlias = null;
            Conta contaAlias = null;

            var dto = session.QueryOver<Conta>(() => contaAlias)
                        .JoinAlias(() => contaAlias.Cliente, () => clienteAlias)
                        .Where(() => clienteAlias.Id == 12)
                        .SelectList(list => list
                            .Select(() => contaAlias.Numero)
                            .Select(() => clienteAlias.Nome)
                         )
                         .TransformUsing(Transformers.AliasToBean<ClienteContaDto>())
                         .SingleOrDefault<ClienteContaDto>();
            session.Close();
            sessionFactory.Close();

        }
    }
}
