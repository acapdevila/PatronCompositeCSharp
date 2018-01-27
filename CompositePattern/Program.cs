using System;
using System.Collections.Generic;

namespace CompositePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ejemplo para la definición del patrón
            PintarUnArbol();
        }

        private static void PintarUnArbol()
        {
            var troncoArbol = new Rama();

            var rama1 = new Rama();
            rama1.Brotar(new Hoja());

            var rama2 = new Rama();
            rama1.Brotar(new Hoja());
            rama1.Brotar(new Hoja());

            var rama3 = new Rama();
            rama3.Brotar(new Hoja());
            rama3.Brotar(new Hoja());
            rama3.Brotar(new Hoja());

            troncoArbol.Brotar(rama1);
            troncoArbol.Brotar(rama2);
            troncoArbol.Brotar(rama3);

            troncoArbol.Pintar(1);

            Console.ReadLine();
        }


        public abstract class ComponenteArbol
        {
            public virtual void Brotar(ComponenteArbol componente)
            {
                throw new NotImplementedException();
            }

            public virtual void Cortar(ComponenteArbol componente)
            {
                throw new NotImplementedException();
            }
            public abstract void Pintar(int nivel);
        }
        
        public class Rama : ComponenteArbol
        {
            private readonly List<ComponenteArbol> _componentes;
            
            public Rama()
            {
                _componentes = new List<ComponenteArbol>();
            }

            public override void Brotar(ComponenteArbol componente)
            {
                _componentes.Add(componente);
            }

            public override void Cortar(ComponenteArbol componente)
            {
                _componentes.Remove(componente);
            }

            public override void Pintar(int nivel)
            {
                Console.WriteLine(new String('-', nivel) + " rama");

                foreach (ComponenteArbol hojaORama in _componentes)
                {
                   hojaORama.Pintar(nivel + 1);
                }
            }
        }
        public class Hoja : ComponenteArbol
        {
            public override void Pintar(int nivel)
            {
                Console.WriteLine(new String('-', nivel) + " hoja");
            }
        }
    }
}
