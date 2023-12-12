namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{
    public async Task<Response<Testimonial>> GetTestimonialsAsync(ODataQuery? query = null) =>
        await MainApiBroker.GetTestimonialsAsync($"Testimonial{query}");
    public async Task<Response<Testimonial>> GetTestimonialByIDAsync(int id, ODataQuery? query = null) =>
       await MainApiBroker.GetTestimonialByIDAsync($"Testimonial/{id}{query}");
    public async Task<Response<Testimonial>> AddTestimonialAsync(Testimonial testimonial) =>
        await MainApiBroker.PostTestimonialAsync("Testimonial", GetPreparedTestimonial(testimonial));
    public async Task<Response<Testimonial>> UpdateTestimonialAsync(int id, Testimonial testimonial) =>
        await MainApiBroker.PutTestimonialAsync($"Testimonial/{id}", GetPreparedTestimonial(testimonial));

    public async Task<Response<Testimonial>> DeleteTestimonialAsync(int id) =>
        await MainApiBroker.DeleteTestimonialAsync($"Testimonial/{id}");

    private static Testimonial GetPreparedTestimonial(Testimonial testimonial)
    {
        var preparedTestimonial = JsonSerializer.Deserialize<Testimonial>(
            JsonSerializer.Serialize(testimonial))!;

        return preparedTestimonial;
    }

}
