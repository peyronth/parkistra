namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{
    public async Task<Response<Testimonials>> GetTestimonialsAsync(ODataQuery? query = null) =>
        await MainApiBroker.GetTestimonialsAsync($"Testimonial{query}");
    public async Task<Response<Testimonials>> GetTestimonialByIDAsync(int id, ODataQuery? query = null) =>
       await MainApiBroker.GetTestimonialByIDAsync($"Testimonial/{id}{query}");
    public async Task<Response<Testimonials>> AddTestimonialAsync(Testimonials Testimonial) =>
        await MainApiBroker.PostTestimonialAsync("Testimonial", GetPreparedTestimonial(Testimonial));
    public async Task<Response<Testimonials>> UpdateTestimonialAsync(int id, Testimonials Testimonial) =>
        await MainApiBroker.PutTestimonialAsync($"Testimonial/{id}", GetPreparedTestimonial(Testimonial));

    public async Task<Response<Testimonials>> DeleteTestimonialAsync(int id) =>
        await MainApiBroker.DeleteTestimonialAsync($"Testimonial/{id}");

    private static Testimonials GetPreparedTestimonial(Testimonials Testimonial)
    {
        var preparedTestimonial = JsonSerializer.Deserialize<Testimonials>(
            JsonSerializer.Serialize(Testimonial))!;

        return preparedTestimonial;
    }

}
