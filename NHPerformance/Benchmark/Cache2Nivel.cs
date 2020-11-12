using System;
namespace NHPerformance
{
    public class Cache2Nivel : SessionFactoryBuild2Cache
    {
        public Cache2Nivel()
        {
        }

        public void TestarCache2Nivel()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            Console.WriteLine("--- Get ---");

            var cliente = session.Get<Cliente>((long)10);
            Console.WriteLine(cliente.Nome);

            var cliente1 = session.Get<Cliente>((long)10);
            Console.WriteLine(cliente1.Nome);

            Console.WriteLine("--- Load ---");

            var cliente2 = session.Load<Cliente>((long)11);
            Console.WriteLine(cliente2.Nome);

            var cliente3 = session.Load<Cliente>((long)11);
            Console.WriteLine(cliente3.Nome);

            Console.WriteLine("--- Query ---");

            var cliente4 = session.QueryOver<Cliente>()
                                .Where(c => c.Id == 12)
                                .SingleOrDefault();
            Console.WriteLine(cliente4.Nome);

            var cliente5 = session.QueryOver<Cliente>()
                                .Where(c => c.Id == 12)
                                .SingleOrDefault();
            Console.WriteLine(cliente5.Nome);


            session.Close();
            sessionFactory.Close();
        }

        public void TestarMultiplasSessionFactories()
        {
            Console.WriteLine("--- Primeira SessionFactory ---");
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            Console.WriteLine("--- Get ---");

            var cliente = session.Get<Cliente>((long)10);
            Console.WriteLine(cliente.Nome);

            session.Close();
            sessionFactory.Close();


            Console.WriteLine("--- Segunda SessionFactory ---");
            using var sessionFactory1 = Build();
            using var session1 = sessionFactory1.OpenSession();

            Console.WriteLine("--- Get ---");

            var cliente1 = session1.Get<Cliente>((long)10);
            Console.WriteLine(cliente1.Nome);

            session1.Close();
            sessionFactory1.Close();
        }

        public void TestarCache2NivelVariasSessions()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            Console.WriteLine("--- Get ---");

            var cliente = session.Get<Cliente>((long)10);
            Console.WriteLine(cliente.Nome);

            var cliente1 = session.Get<Cliente>((long)10);
            Console.WriteLine(cliente1.Nome);

            session.Close();

            using var session1 = sessionFactory.OpenSession();

            Console.WriteLine("--- Get ---");

            var cliente2 = session1.Get<Cliente>((long)10);
            Console.WriteLine(cliente2.Nome);

            var cliente3 = session1.Get<Cliente>((long)10);
            Console.WriteLine(cliente3.Nome);

            session1.Close();

            sessionFactory.Close();
        }

        public void TestarCache2NivelQueryCache()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            //var cliente = session.QueryOver<Cliente>()
            //                    .Where(c => c.Id == 12)
            //                    .Cacheable()
            //                    .SingleOrDefault();
            //Console.WriteLine(cliente.Nome);

            //var cliente1 = session.QueryOver<Cliente>()
            //                    .Where(c => c.Id == 12)
            //                    .Cacheable()
            //                    .SingleOrDefault();
            //Console.WriteLine(cliente1.Nome);

            var cliente3 = session.Get<Cliente>((long)12);
            Console.WriteLine(cliente3.Nome);

            session.Close();
            sessionFactory.Close();

        }

        public void TestarCache2NivelLimaprCache()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            var cliente = session.QueryOver<Cliente>()
                                .Where(c => c.Id == 12)
                                .Cacheable()
                                .SingleOrDefault();
            Console.WriteLine(cliente.Nome);

            sessionFactory.Evict(typeof(Cliente), 12);

            var cliente1 = session.QueryOver<Cliente>()
                                .Where(c => c.Id == 12)
                                .Cacheable()
                                .SingleOrDefault();
            Console.WriteLine(cliente1.Nome);

            session.Close();
            sessionFactory.Close();

        }
    }
}
