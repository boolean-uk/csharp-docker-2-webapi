using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

namespace exercise.wwwapi.EndPoint
{
    public static class PublisherApi
    {
        public static void ConfigurePublisherApi(this WebApplication app)
        {
            app.MapGet("/publishers", GetPublishers);
            //app.MapGet("/authors/{id}", GetPublisher);
            //app.MapDelete("/authors", DeletePublisher);
        }
        private static async Task<IResult> GetPublishers(IDatabaseRepository<Publisher> service)
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
