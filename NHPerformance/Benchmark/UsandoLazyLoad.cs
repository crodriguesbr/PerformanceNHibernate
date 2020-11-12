using System;
using NHibernate;

namespace NHPerformance
{
    public class UsandoLazyLoad: SessionFactoryBuild
    {
        public UsandoLazyLoad()
        {
        }

        public void LazyLoad()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            var conta = session.Get<Conta>((long)1);
            Console.WriteLine(conta.Cliente.Nome);

            session.Close();
            sessionFactory.Close();
        }

        public void PopularLancamentos()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            var conta = session.Get<Conta>((long)1);

            for (int i = 0; i < 20000; i++)
            {
                conta.Creditar(10M);
                session.SaveOrUpdate(conta);
            }

            transaction.Commit();
            session.Close();
            sessionFactory.Close();
        }

        public void FazendoCountNaLista()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            var conta = session.Get<Conta>((long)1);

            Console.WriteLine(conta.QuantidadeLancamentos());

           
            session.Close();
            sessionFactory.Close();
        }

        public void FazendoFetch()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();
            using var transaction = session.BeginTransaction();

            var conta = session.QueryOver<Conta>()
                        .Fetch(SelectMode.JoinOnly, c => c.Cliente)
                        .Where(c => c.Id == 1)
                        .SingleOrDefault();

            Console.WriteLine(conta.Cliente.Nome);


            session.Close();
            sessionFactory.Close();
        }
    }
}
