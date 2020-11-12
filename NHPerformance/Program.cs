using System;
using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Running;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NHPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("--- Iniciando ---");
                //var summary = BenchmarkRunner.Run<SessionVsStatelessSession>();
                var chaves = new ChavesPrimarias();
                //chaves.TestarCache1Nivel();
                //chaves.GetVsLoad();
                var usandoLazyLoad = new UsandoLazyLoad();
                //usandoLazyLoad.LazyLoad();
                //usandoLazyLoad.PopularLancamentos();
                //usandoLazyLoad.FazendoCountNaLista();
                //usandoLazyLoad.FazendoFetch();
                //var otimizandoConsultas = new OtimizandoConsultas();
                //otimizandoConsultas.UsandoDto();
                var cache2Nivel = new Cache2Nivel();
                //cache2Nivel.TestarCache2Nivel();
                //cache2Nivel.TestarCache2NivelVariasSessions();
                //cache2Nivel.TestarCache2NivelQueryCache();
                cache2Nivel.TestarMultiplasSessionFactories();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        
    }
}
