using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using NHibernate;

namespace NHPerformance
{
    [RPlotExporter, RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class SessionVsStatelessSession : SessionFactoryBuild
    {
        [Benchmark]
        public void PerformanceISession()
        {
            using var sessionFactory = this.Build();
            using var session = sessionFactory.OpenSession();
            using var transactionSession = session.BeginTransaction();
            for (int i = 0; i < 100; i++)
            {
                session.Save(new Cliente($"Cliente-{Guid.NewGuid()}"));
            }
            transactionSession.Commit();
            session.Close();
            sessionFactory.Close();
        }

        [Benchmark]
        public void PerformanceIStatelessSession()
        {
            using var sessionFactory = this.Build();
            using var statelessSession = sessionFactory.OpenStatelessSession();
            using var transactionStateless = statelessSession.BeginTransaction();
            for (int i = 0; i < 100; i++)
            {
                statelessSession.Insert(new Cliente($"Cliente-{Guid.NewGuid()}"));
            }
            transactionStateless.Commit();
            statelessSession.Close();
            sessionFactory.Close();
        }
    }
}
