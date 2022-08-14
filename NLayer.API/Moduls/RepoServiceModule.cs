using System.Reflection;
using Autofac;
using Nlayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Services.Mapping;
using NLayer.Services.Services;
using Module = Autofac.Module;

namespace NLayer.API.Moduls;

public class RepoServiceModule :Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //Generic olan sınıflarımızı ve interface'lerimiz bu sekilde ekleriz.
        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
        //generic olmayanları da Bu fonksiyon ile ekleyebiliriz. Zaten isimden anlaşılıyor. Generic önemli bir mimari dikkat etmek gerekiyor.
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        //bu yapıyı kullanmak hem daha verimli hemde startup dosyasını daha az kod ile - temiz yazmıs  olacağız.


        /*once assembly'leri alalım */
        var apiAssembly = Assembly.GetExecutingAssembly(); //api'nin assembly'sini getirir.
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));
        //var serviceAssembly = Assembly.GetAssembly(typeof(Service<>));

        //her katmanın assembly'lerini aldık. 
        
        /*
         * AutoFac kütüphanesinin olayı şudur :  Bizim örneğin IProductService ile ProductService isimli
         * sınıf ve interface'i startup da servise ekliyoruz. İşte bu durumda bütün interface ve bütün
         * sınıfları tek tek eklemek yerine bunu belirli bir kriter altında
         * örneğin sonu service ile biten interface ve sınıfları birleştirir der gibi bir ortak property
         * ile bu nesne ve interface'i dinamik olarak eşleştirebiliyoruz. 
         */

       
        
        
        //Cogul olarak olan interface-class ikililerinde bunu yapabiliriz Tekil olanlarda örneğin db context gibi nesnelerde 
        //bunu elle ekleriz. Manuel. Bir sonraki örnek de o olucak.
        builder.RegisterAssemblyTypes(apiAssembly, serviceAssembly, repoAssembly)
            .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


        builder.RegisterAssemblyTypes(apiAssembly, serviceAssembly, repoAssembly)
           .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

        //instanceperfilestimescope => Scope lifetime => sadece bir instance ile diğer bütün interface'ler kullanılır.
        //instancePerDependency => Transient LifeTime


        //Added Cache structure
        //builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();


    }
}