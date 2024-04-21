using System.Collections.Generic;
using Autofac;
using Kassasystemet___Mille_Elfver.Interfaces;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var container = AutoFacRegistration.RegisteredContainers())
            {
                var productServices = container.Resolve<IProductServices>();
                MainMenu.Menu(productServices);
            }

        }

    }

}
