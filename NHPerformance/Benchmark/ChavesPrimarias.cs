using System;
namespace NHPerformance
{
    public class ChavesPrimarias: SessionFactoryBuild
    {
        public ChavesPrimarias()
        {
        }

        public void TestarCache1Nivel()
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

        public void GetVsLoad()
        {
            using var sessionFactory = Build();
            using var session = sessionFactory.OpenSession();

            //var cliente = session.Get<Cliente>((long)10);
            //Console.WriteLine(cliente.Nome);

            //var cliente1 = session.Load<Cliente>((long)11);
            //Console.WriteLine(cliente1.Nome);

            //Console.WriteLine(cliente1.GetType());

            //Console.WriteLine("--- Como se beneficiar do Load ---");

            var clienteLoad = session.Load<Cliente>((long)10);
            var conta = new Conta() { Cliente = clienteLoad, Numero = 1001 };
            session.SaveOrUpdate(conta);



            session.Close();
            sessionFactory.Close();
        }
    }
}
