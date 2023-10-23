using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.EndPoint
{
    public static class BookApi
    {
        public static void ConfigureBookApi(this WebApplication app)
        {
            app.MapGet("/books", GetBooks);
            //app.MapGet("/authors/{id}", GetPublisher);
            //app.MapDelete("/authors", DeletePublisher);
        }
        private static async Task<IResult> GetBooks(IDatabaseRepository<Book> service)
        {
            try
            {
                return await Task.Run(() => {
                    return Results.Ok(service.GetAll());
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
