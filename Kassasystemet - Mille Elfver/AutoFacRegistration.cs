using Autofac;
using Kassasystemet___Mille_Elfver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public static class AutoFacRegistration
    {
        public static IContainer RegisteredContainers()
        {
            var myContainerBuilder = new ContainerBuilder();

            myContainerBuilder.RegisterType<ProductCatalog>().As<IProductCatalog>().SingleInstance();
            myContainerBuilder.RegisterType<ProductServices>().As<IProductServices>();
            myContainerBuilder.RegisterType<FileManager>().As<IFileManager>();
            myContainerBuilder.RegisterType<ReceiptCounter>().AsSelf();
            myContainerBuilder.RegisterType<ReceiptCreation>().AsSelf();

            return myContainerBuilder.Build();
        }
    }
}
