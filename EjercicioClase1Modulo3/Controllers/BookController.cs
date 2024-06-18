using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EjercicioClase1Modulo3.Controllers
{
    [Route( "" )]
    [ApiController]
    public class BookController : ControllerBase
    {
        //Books contiene una lista de libros. Esta información viene del archivo libros.json ubicado dentro de la carpeta Data.
        public List<Book> Books { get; set; }

        //filePath contiene la ubicación del archivo libros.json. No mover el archivo libros.json de esa carpeta.
        string filePath = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, @"Data\libros.json" );

        public BookController()
        {
            //Instanciación e inicialización de la lista de libros deserializando el archivo libros.json
            Books = JsonSerializer.Deserialize<List<Book>>( System.IO.File.ReadAllText( filePath ) );
        }

        #region Ejercicio 1
        /*
        Completar y modificar el método siguiente para crear un endpoint que liste todos los libros y tenga la siguiente estructura:
        [GET] v1/libros
        */

        // [HttpGet]
        // [Route( "v1/libros" )]
        // public ActionResult<List<Book>> GetBooks()
        // {
        //     
        //     return Books;
        //   
        // }

        #endregion

        #region Ejercicio 2
        /*
         Crear un endpoint para Obtener un libro por su número de id usando route parameters que tenga la siguiente estructura:
        [GET] v1/libros/{id}
        Ejemplo: v1/libros/8 (devuelve toda la información del libro cuyo id es 8. Es decir: El diario de Ana Frank)
        */
        
        [HttpGet]
        [Route( "v1/libros/{id}" )]
        public ActionResult<Book> GetBookById( int id )
        {
            var book = Books.FirstOrDefault( x => x.id == id );
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }


        #endregion

        #region Ejercicio 3
        /*
         Crear un endpoint para listar todos libros de un género en particular usando route parameters que tenga la siguiente estructura:
        [GET] v1/libros/genero/{genero} 
        Ejemplo: v1/libros/genero/fantasía (devuelve una lista de todos los libros del género fantasía)
         */
        
        [HttpGet]
        [Route( "v1/libros/genero/{genero}" )]
        public ActionResult<List<Book>> GetBooksByGenre( string genero )
        {
            var books = Books.Where( x => x.genero.ToLower() == genero.ToLower() ).ToList();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return books;
        }


        #endregion

        #region Ejercicio 4
        /*
        Crear un endpoint para Listar todos los libros de un autor usando query parameters que tenga la siguiente estructura:
        [GET] v1/libros?autor={autor}
        Ejemplo: v1/libros?autor=Paulo Coelho (devuelve una lista de todos los libros del autor Paulo Coelho)
         */
        
        // [HttpGet]
        // [Route( "v1/libros" )]
        // public ActionResult<List<Book>> GetBooksByAuthor( string autor )
        // {
        //     var books = Books.Where( x => x.autor.ToLower() == autor.ToLower() ).ToList();
        //     if (books.Count == 0)
        //     {
        //         return NotFound();
        //     }
        //     return books;
        // }

        #endregion

        #region Ejercicio 5
        /*
        Crear un endpoint para Listar unicamente todos los géneros de libros disponibles que tenga la siguiene estructura:
        [GET] v1/libros/generos
        Idealmente, el listado de géneros que retorne el endpoint, no debe contener repetidos.
         */
        
        [HttpGet]
        [Route( "v1/libros/generos" )]
        public ActionResult<List<string>> GetGenres()
        {
            var genres = Books.Select( x => x.genero ).Distinct().ToList();
            return genres;
        }

        #endregion

        #region Ejercicio 6
        /*
        Crear un endpoint para listar todos los libros implementando paginación usando route parameters con la siguiente estructura:
        [GET] v1/libros?pagina={numero-pagina}&cantidad={cantidad-por-pagina}
        Ejemplos: 
        v1/libros?pagina=1&cantidad=10 (devuelve una lista de los primeros diez libros)
        v1/libros?pagina=2&cantidad=10 (devuelve una lista de diez libros, salteando los primeros 10)
        v1/libros?pagina=3&cantidad=10 (devuelve una lista de diez libros, salteando los primeros 20)
         */
        
        [HttpGet]
        [Route( "v1/libros" )]
        public ActionResult<List<Book>> GetBooksByPage( int pagina, int cantidad )
        {
            var books = Books.Skip( ( pagina - 1 ) * cantidad ).Take( cantidad ).ToList();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return books;
        }
        
        #endregion
    }
}
