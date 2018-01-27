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

            // Ejemplo de costes
            CalcularCostesDeUnProductoFormadoPorOtrosProductos();

            Console.ReadLine();
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

        private static void CalcularCostesDeUnProductoFormadoPorOtrosProductos()
        {
            var conjuntoB = CrearConjuntoB();
            
            Console.WriteLine("Coste de " + conjuntoB.Nombre + ":");
            Console.WriteLine(conjuntoB.CalcularCoste());
        }

        private static Componente CrearConjuntoB()
        {
            var referenciaB = new Referencia("B", 4);
            var conjuntoB = new Componente(referenciaB);

            var referenciaB1 = new Referencia("B1", 1);
            var piezaB1 = new Componente(referenciaB1);

            var referenciaB2 = new Referencia("B2", 2);
            var conjuntoB2 = new Componente(referenciaB2);

            var referenciaB21 = new Referencia("B21", 1);
            var piezaB21 = new Componente(referenciaB21);

            var referenciaB22 = new Referencia("B22", 2);
            var piezaB22 = new Componente(referenciaB22);

            conjuntoB2.Añadir(4, piezaB21);
            conjuntoB2.Añadir(2, piezaB22);

            conjuntoB.Añadir(5, piezaB1);
            conjuntoB.Añadir(3, conjuntoB2);

            return conjuntoB;
        }

        public class Referencia
        {
            public Referencia(string nombre, decimal coste)
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new ArgumentException();
                }

                Nombre = nombre;
                Coste = coste;
            }
            public string Nombre { get;  }
            public decimal Coste { get; }
        }

            public class Componente
            {
                private readonly Referencia _referencia;
                private readonly List<Componente> _subComponentes;


            public string Nombre
            {
                get { return _referencia.Nombre;  }
            }

            public Componente(Referencia referencia)
            {
                _referencia = referencia;
                _subComponentes = new List<Componente>();
            }
            
            public void Añadir(int cantidad, Componente componente)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    _subComponentes.Add(componente);
                }
            }

            public void Quitar(Componente componente)
            {
                _subComponentes.Remove(componente);
            }
            
            public decimal CalcularCoste()
            {
                decimal coste = _referencia.Coste;

                foreach (var componenteProducto in _subComponentes)
                {
                    coste = coste + componenteProducto.CalcularCoste();
                }

                return coste;
            }
        }
        
    }
}
