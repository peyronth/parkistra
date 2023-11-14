namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{
    public async Task<Response<Testimonials>> GetTestimonialsAsync(ODataQuery? query = null) =>
        await MainApiBroker.GetTestimonialsAsync($"Testimonials{query}");
    public async Task<Response<Testimonials>> GetTestimonialByIDAsync(int id, ODataQuery? query = null) =>
       await MainApiBroker.GetTestimonialByIDAsync($"Testimonials/{id}{query}");
    public async Task<Response<Testimonials>> AddTestimonialAsync(Testimonials Testimonial) =>
        await MainApiBroker.PostTestimonialAsync("Testimonials", GetPreparedTestimonial(Testimonial));
    public async Task<Response<Testimonials>> UpdateTestimonialAsync(int id, Testimonials Testimonial) =>
        await MainApiBroker.PutTestimonialAsync($"Testimonials/{id}", GetPreparedTestimonial(Testimonial));

    public async Task<Response<Testimonials>> DeleteTestimonialAsync(int id) =>
        await MainApiBroker.DeleteTestimonialAsync($"Testimonials/{id}");

    private static Testimonials GetPreparedTestimonial(Testimonials Testimonial)
    {
        var preparedTestimonial = JsonSerializer.Deserialize<Testimonials>(
            JsonSerializer.Serialize(Testimonial))!;

        return preparedTestimonial;
    }

}
