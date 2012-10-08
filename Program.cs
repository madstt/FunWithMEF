
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace FunWithMEF
{
    class Program
    {
        private CompositionContainer container;

        [Import(typeof (ICalculator))] 
        public ICalculator Calculator;
        
        private Program()
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        static void Main(string[] args)
        {
            var program = new Program();
            while (true)
            {
                var s = Console.ReadLine();
                Console.WriteLine(program.Calculator.Calculate(s));
            }
        }


    }
}
