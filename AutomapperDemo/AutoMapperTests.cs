using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomapperDemo.Models;
using AutoMapper;
using NUnit.Framework;

namespace AutomapperDemo
{
    [TestFixture]
    public class AutoMapperTests
    {

        [Test]
        public void Automapper_Fails_When_No_Mapping_Is_Specified()
        {
            A a = new A();

            B b = AutoMapper.Mapper.Map<B>(a);
        }


        [Test]
        public void Automapper_Can_Map_One_Class_To_Another()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();

            B b = mapper.Map<B>(a);

            Assert.AreEqual(a.DateCreated, b.DateCreated);
            Assert.AreEqual(a.Name, b.Name);
            Assert.AreEqual(a.Value, b.Value);
        }


        [Test]
        public void Automapper_Can_Map_One_Class_To_Another_Base_Class()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();

            BBase bbase = mapper.Map<BBase>(a);

            Assert.AreEqual(a.Seite, bbase.Seite);
        }

        [Test]
        public void Automapper_Can_Map_One_Class_To_Another_With_Manual_MapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<A, B>();
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();
            B b = mapper.Map<B>(a);

            Assert.AreEqual(a.Name, b.Name);
        }


        [Test]
        public void Automapper_Can_Map_One_Class_To_Base_Of_Another_With_Manual_MapperConfig_And_Child_Class_Is_Returned()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<A, B>();

                cfg.CreateMap<ABase, BBase>()
                    .Include<A, B>();
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();
            BBase b = mapper.Map<BBase>(a);
        }


        [Test]
        public void Automapper_Can_Map_One_Class_To_Another_With_Manual_MapperConfig_And_Different_PropertyNames()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<A, B>()
                .ForMember(d => d.Wohnort,
                           o => o.MapFrom(s => s.Adresse));
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();

            B b = mapper.Map<B>(a);

            Assert.AreEqual(a.Adresse, b.Wohnort);
        }


        [Test]
        public void Automapper_Can_Map_One_Class_To_Another_And_Reverse_With_Manual_MapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<A, B>()
                .ForMember(d => d.Wohnort,
                           o => o.MapFrom(s => s.Adresse))
                .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();
            B b = mapper.Map<B>(a);
            A aZwei = mapper.Map<A>(b);

            Assert.AreEqual(a.Adresse, aZwei.Adresse);
            Assert.AreEqual(a.Name, aZwei.Name);
        }


        [Test]
        public void Automapper_Can_Map_Lists_Of_Base_Classes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<A, B>();

                cfg.CreateMap<X, Z>();

                cfg.CreateMap<ABase, BBase>()
                    .Include<A, B>()
                    .Include<X, Z>();
            });

            IMapper mapper = config.CreateMapper();

            A a = new A();
            X x = new X();

            List<ABase> aBaseList = new List<ABase>() { a, x };

            List<BBase> bBaseList = mapper.Map<List<BBase>>(aBaseList);

            Assert.IsNotNull(bBaseList.FirstOrDefault(t => t.GetType() == typeof(B)));
            Assert.IsNotNull(bBaseList.FirstOrDefault(t => t.GetType() == typeof(Z)));
        }


        /**
         * 
         *    Lazy Loading mit NHibernate mit Automapper ist aufgrund der Auflösung aller Properties keine Gute Idee. Der Automapper
         *    durchläuft jede einzelne Property und falls eine dieser Property per Lazy Loading geladen wird, wird diese während des
         *    Automappings nachgeladen, was zu den bekannten "N+1" warnings des NHibernate Profilers führt.
         * 
         * */
    }
}
