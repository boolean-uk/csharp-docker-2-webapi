using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.EndPoint
{
    public static class AuthorApi
    {

        public static void ConfigureAuthorApi(this WebApplication app)
        {
            app.MapGet("/authors", GetAuthors);
            app.MapGet("/authors/{id}", GetAuthor);
            app.MapPost("/authors", InsertAuthor);
            app.MapPut("/authors", UpdateAuthor);
            app.MapDelete("/authors", DeleteAuthor);
        }
        private static async Task<IResult> GetAuthors(IDatabaseRepository<Author> service)
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
        private static async Task<IResult> GetAuthor(int id, IDatabaseRepository<Author> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var entity = service.GetById(id);
                    if (entity == null) return Results.NotFound();
                    return Results.Ok(entity);
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> InsertAuthor(Author model, IDatabaseRepository<Author> service)
        {
            try
            {
                service.Insert(model);
                service.Save();
                return Results.Ok();                

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> UpdateAuthor(Author author, IDatabaseRepository<Author> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (service.GetById(author.Id) != null)
                    {    
                        service.Update(author);
                        service.Save();
                        return Results.Ok();
                    }
                    return Results.NotFound();
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> DeleteAuthor(int id, IDatabaseRepository<Author> service)
        {
            try
            {
                if (service.GetById(id) != null)
                {
                    service.Delete(id);
                    service.Save();
                    return Results.Ok();
                }

                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}