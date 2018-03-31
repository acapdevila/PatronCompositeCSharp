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

            CalcularCostesDeUnProductoIntento1();

            CalcularCostesDeUnProductoIntento2();

            CalcularCostesDeUnProductoIntento3();

            Console.ReadLine();
        }

        #region Ejemplo clásico

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
            public abstract void Brotar(ComponenteArbol componente);

            public abstract void Cortar(ComponenteArbol componente);

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
            public override void Brotar(ComponenteArbol componente)
            {
                throw new NotImplementedException();
            }

            public override void Cortar(ComponenteArbol componente)
            {
                throw new NotImplementedException();
            }

            public override void Pintar(int nivel)
            {
                Console.WriteLine(new String('-', nivel) + " hoja");
            }
        }

        #endregion


        #region Ejemplo primer intento

        private static void CalcularCostesDeUnProductoIntento1()
        {
            var conjuntoB = CrearConjuntoB();

            Console.WriteLine("Coste de " + conjuntoB.Nombre + ":");
            Console.WriteLine(conjuntoB.CalcularCoste(1));
        }

        private static Conjunto CrearConjuntoB()
        {
            var referenciaB = new Referencia("B", coste: 4);
            var conjuntoB = new Conjunto(referenciaB);

            var referenciaB1 = new Referencia("B1", coste: 1);
            var piezaB1 = new Pieza(referenciaB1);

            var referenciaB2 = new Referencia("B2", coste: 2);
            var conjuntoB2 = new Conjunto(referenciaB2);

            var referenciaB21 = new Referencia("B21", coste: 1);
            var piezaB21 = new Pieza(referenciaB21);

            var referenciaB22 = new Referencia("B22", coste: 2);
            var piezaB22 = new Pieza(referenciaB22);

            conjuntoB2.Añadir(3, piezaB21);
            conjuntoB2.Añadir(2, piezaB22);

            conjuntoB.Añadir(5, piezaB1);
            conjuntoB.Añadir(3, conjuntoB2);

            return conjuntoB;
        }


        public abstract class Componente
        {
            public abstract string Nombre { get; }

            public abstract void Añadir(Componente componente);

            public abstract void Quitar(Componente componente);

            public abstract decimal CalcularCoste(int nivel);
        }

        public class Referencia
        {
            public Referencia(string nombre, decimal coste)
            {
                if (string.IsNullOrEmpty(nombre))
                    throw new ArgumentException(nameof(nombre));

                if (coste < 0)
                    throw new ArgumentException(nameof(coste));

                Nombre = nombre;
                Coste = coste;
            }
            public string Nombre { get; }
            public decimal Coste { get; }
        }


        public class Pieza : Componente
        {
            private readonly Referencia _referencia;

            public Pieza(Referencia referencia)
            {
                _referencia = referencia;
            }

            public override string Nombre => _referencia.Nombre;

            public override void Añadir(Componente componente)
            {
                throw new NotImplementedException();
            }

            public override void Quitar(Componente componente)
            {
                throw new NotImplementedException();
            }

            public override decimal CalcularCoste(int nivel)
            {
                Console.WriteLine(new String('-', nivel) + " Pieza: " + Nombre + " - Coste:" + _referencia.Coste);

                return _referencia.Coste;
            }
        }

        public class Conjunto : Componente
        {
            private readonly Referencia _referencia;
            private readonly List<Componente> _subComponentes;


            public override string Nombre
            {
                get { return _referencia.Nombre; }
            }

            public override void Añadir(Componente componente)
            {
                _subComponentes.Add(componente);
            }

            public override void Quitar(Componente componente)
            {
                _subComponentes.Remove(componente);
            }

            public Conjunto(Referencia referencia)
            {
                _referencia = referencia;
                _subComponentes = new List<Componente>();
            }

            public void Añadir(int cantidad, Componente componente)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    Añadir(componente);
                }
            }


            public override decimal CalcularCoste(int nivel)
            {
                decimal coste = _referencia.Coste;

                Console.WriteLine(new String('-', nivel) + " " + Nombre + ": " + coste);

                foreach (var componenteProducto in _subComponentes)
                {
                    coste = coste + componenteProducto.CalcularCoste(nivel + 1);
                }

                return coste;
            }
        }



#endregion


        #region Ejemplo segundo intento

        private static void CalcularCostesDeUnProductoIntento2()
        {
            var conjunto = CrearConjuntoSegundoIntento();

            Console.WriteLine("Coste de " + conjunto.Nombre + ":");
            Console.WriteLine(conjunto.CalcularCoste(1));
        }

        private static ComponenteIntento2 CrearConjuntoSegundoIntento()
        {
            var referenciaB = new Referencia("B", 4);
            var conjuntoB = new ComponenteIntento2(referenciaB);

            var referenciaB1 = new Referencia("B1", 1);
            var piezaB1 = new ComponenteIntento2(referenciaB1);

            var referenciaB2 = new Referencia("B2", 2);
            var conjuntoB2 = new ComponenteIntento2(referenciaB2);

            var referenciaB21 = new Referencia("B21", 1);
            var piezaB21 = new ComponenteIntento2(referenciaB21);

            var referenciaB22 = new Referencia("B22", 2);
            var piezaB22 = new ComponenteIntento2(referenciaB22);

            conjuntoB2.Añadir(3, piezaB21);
            conjuntoB2.Añadir(2, piezaB22);

            conjuntoB.Añadir(5, piezaB1);
            conjuntoB.Añadir(3, conjuntoB2);

            return conjuntoB;
        }

        public class ComponenteIntento2
        {
            private readonly Referencia _referencia;
            private readonly List<ComponenteIntento2> _subComponentes;


            public string Nombre
            {
                get { return _referencia.Nombre; }
            }

            public void Añadir(ComponenteIntento2 componente)
            {
                _subComponentes.Add(componente);
            }

            public void Quitar(ComponenteIntento2 componente)
            {
                _subComponentes.Remove(componente);
            }

            public ComponenteIntento2(Referencia referencia)
            {
                _referencia = referencia;
                _subComponentes = new List<ComponenteIntento2>();
            }

            public void Añadir(int cantidad, ComponenteIntento2 componente)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    Añadir(componente);
                }
            }


            public decimal CalcularCoste(int nivel)
            {
                decimal coste = _referencia.Coste;

                Console.WriteLine(new String('-', nivel) + " " + Nombre + ": " + coste);

                foreach (var componenteProducto in _subComponentes)
                {
                    coste = coste + componenteProducto.CalcularCoste(nivel + 1);
                }

                return coste;
            }
        }

        #endregion

        #region Ejemplo tercer intento

        private static void CalcularCostesDeUnProductoIntento3()
        {
            var conjunto = CrearConjuntoTercerIntento();

            Console.WriteLine("Coste de " + conjunto.Nombre + ":");
            Console.WriteLine(conjunto.CalcularCoste(1));
        }

        private static ComponenteIntento3 CrearConjuntoTercerIntento()
        {
            var referenciaB = new Referencia("B", 4);
            var conjuntoB = new ComponenteIntento3(referenciaB);

            var referenciaB1 = new Referencia("B1", 1);
            var piezaB1 = new ComponenteIntento3(referenciaB1);

            var referenciaB2 = new Referencia("B2", 2);
            var conjuntoB2 = new ComponenteIntento3(referenciaB2);

            var referenciaB21 = new Referencia("B21", 1);
            var piezaB21 = new ComponenteIntento3(referenciaB21);

            var referenciaB22 = new Referencia("B22", 2);
            var piezaB22 = new ComponenteIntento3(referenciaB22);

            conjuntoB2.Añadir(3, piezaB21);
            conjuntoB2.Añadir(2, piezaB22);

            conjuntoB.Añadir(5, piezaB1);
            conjuntoB.Añadir(3, conjuntoB2);

            return conjuntoB;
        }

        public class ComponenteIntento3
        {
            private readonly Referencia _referencia;
            private readonly List<ComponenteIntento3> _subComponentes;

            public int Cantidad { get; set; }

            public string Nombre
            {
                get { return _referencia.Nombre; }
            }

            public void Añadir(ComponenteIntento3 componente)
            {
                _subComponentes.Add(componente);
            }

            public void Quitar(ComponenteIntento3 componente)
            {
                _subComponentes.Remove(componente);
            }

            public ComponenteIntento3(Referencia referencia)
            {
                _referencia = referencia;
                _subComponentes = new List<ComponenteIntento3>();
                Cantidad = 1;
            }

            public void Añadir(int cantidad, ComponenteIntento3 componente)
            {
                componente.Cantidad = cantidad;
                Añadir(componente);
                
            }


            public decimal CalcularCoste(int nivel)
            {
                decimal coste = _referencia.Coste;

                Console.WriteLine(new String('-', nivel) + $" {Nombre} - Coste: {coste} - Cantidad: {Cantidad}");

                foreach (var componenteProducto in _subComponentes)
                {
                    coste = coste + componenteProducto.CalcularCoste(nivel + 1) 
                                    * componenteProducto.Cantidad;
                }

                return coste;
            }
        }

        #endregion



    }
}
