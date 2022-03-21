using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Image_Colour_Swap.Interfaces;

public class AWSS3ImageSaver : IImageSaver
{
    public AmazonS3Client _client;

    public AWSS3ImageSaver()
    {
        _client = new AmazonS3Client(RegionEndpoint.EUWest2);
    }

    public async Task<bool> SaveAsync(string filename, Stream imageStream)
    {
        try
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = "imagecolourswap",
                ContentType = "text/plain",
                InputStream = imageStream,
                Key = filename
            };

            var putResponse = await _client.PutObjectAsync(putRequest);

            return putResponse.HttpStatusCode == HttpStatusCode.OK;
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error saving {filename}");
            Console.Error.WriteLine(ex.Message);

            return false;            
        }
    }
}