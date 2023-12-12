namespace ParkIstra.AppServices.MainApi;

public partial class MainApiService
{
    public async Task<Response<Image>> GetImagesAsync(ODataQuery? query = null) =>
        await MainApiBroker.GetImagesAsync($"Image{query}");
    public async Task<Response<Image>> GetImageByIDAsync(int id, ODataQuery? query = null) =>
       await MainApiBroker.GetImageByIDAsync($"Image/{id}{query}");
    public async Task<Response<Image>> AddImageAsync(Image image) =>
        await MainApiBroker.PostImageAsync("Image", GetPreparedImage(image));
    public async Task<Response<Image>> UpdateImageAsync(int id, Image image) =>
        await MainApiBroker.PutImageAsync($"Image/{id}", GetPreparedImage(image));

    public async Task<Response<Image>> DeleteImageAsync(int id) =>
        await MainApiBroker.DeleteImageAsync($"Image/{id}");

    private static Image GetPreparedImage(Image image)
    {
        var preparedImage = JsonSerializer.Deserialize<Image>(
            JsonSerializer.Serialize(image))!;

        return preparedImage;
    }

}
