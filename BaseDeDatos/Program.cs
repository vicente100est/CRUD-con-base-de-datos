using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BaseDeDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BeerDB beerDb = new BeerDB("DESKTOP-U1F6TFI", "CsharpDB", true);
                bool again = true;
                int op = 0;

                do
                {
                    ShowMenu();
                    Console.WriteLine("Elige una opción: ");
                    op = int.Parse(Console.ReadLine());

                    switch (op)
                    {
                        case 1:
                            Show(beerDb);
                            break;
                        case 2:
                            Add(beerDb);
                            break;
                        case 3:
                            Edit(beerDb);
                            break;
                        case 4:
                            Delete(beerDb);
                            break;
                        case 5:
                            again = false;
                            break;
                    }
                } while (again);

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"No se pudo realizar la conexion {ex}");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Ups, parece que hay un error en {ex}");
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("\n---------- MENU ----------");
            Console.WriteLine("1.- Mostrar | 2.- Agregar");
            Console.WriteLine("3.- Editar  | 4.- Eliminar");
            Console.WriteLine("5.- Salir");
        }

        public static void Show(BeerDB beerDb)
        {
            Console.Clear();
            Console.WriteLine("Cervezas disponibles");
            List<Beer> beers = beerDb.GetAll();

            foreach (var beer in beers)
            {
                Console.WriteLine($"Id: {beer.BeerID}, Nombre: {beer.Name}");
            }
        }

        public static void Add(BeerDB beerDB)
        {
            string name;
            int brandId;

            Console.Clear();
            Console.WriteLine("\n---------- Agrega nueva Cerveza ----------");
            Console.WriteLine("Ingresa el nombre: ");
            name = Console.ReadLine();
            Console.WriteLine("Ingresa el ID de una marca: ");
            brandId = int.Parse(Console.ReadLine());
            Beer beer = new Beer(name, brandId);
            beerDB.Add(beer);
        }

        public static void Edit(BeerDB beerDB)
        {
            int id;
            string name;
            int brand;
            Console.Clear();
            Show(beerDB);
            Console.WriteLine("\n---------- Editar Cerveza ----------");
            Console.WriteLine("Escribe el ID de tu cerveza a editar: ");
            id = int.Parse(Console.ReadLine());

            Beer beer = beerDB.Get(id);
            if (beer != null)
            {
                Console.WriteLine("Escribe el nombre: ");
                name = Console.ReadLine();
                Console.WriteLine("Escibre el ID de la marca: ");
                brand = int.Parse(Console.ReadLine());

                beer.Name = name;
                beer.BrandID = brand;
                beerDB.Edit(beer);
            }
            else
            {
                Console.WriteLine("La cerveza no existe");
            }
        }

        public static void Delete(BeerDB beerDB)
        {
            int id;
            Console.Clear();
            Show(beerDB);
            Console.WriteLine("\n---------- Eliminar Cerveza ----------");
            Console.WriteLine("Escribe el ID de tu cerveza a eliminar: ");
            id = int.Parse(Console.ReadLine());

            Beer beer = beerDB.Get(id);
            if (beer != null)
            {
                beerDB.Delete(id);
            }
            else
            {
                Console.WriteLine("La cerveza no existe");
            }
        }
    }
}
