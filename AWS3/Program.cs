using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;

namespace AWS3
{
    class Program
    {
        static void Main(string[] args)
        {
            string imagePath = "{ image path from local disk}";
            FileStream image = File.Open(imagePath, FileMode.Open);
            AmazonS3Client client = new AmazonS3Client(AWSConfiguration.accessKey, AWSConfiguration.secretKey, AWSConfiguration.bucketRegion);
            UploadImage(client, image, imagePath);

        }
        public static void UploadImage(AmazonS3Client client, FileStream image, string path)
        {
            PutObjectRequest request = new PutObjectRequest()
            {
                InputStream = image,
                BucketName = AWSConfiguration.bucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = "{ path with extension you want to display on url like  https://{bucketName}.s3.amazonaws.com/{key}}"
            };
            try 
            {
                var response = client.PutObjectAsync(request).GetAwaiter().GetResult();
                Console.WriteLine("Uploaded Succesfully");
            }
            catch(AmazonS3Exception amazonS3Exception)
            {
                Console.WriteLine(amazonS3Exception.Message);
            }
        }
    }
    public static class AWSConfiguration
    {
        public static string accessKey = "{ created user's access key }";
        public static string secretKey = "{ created user's secret key }";
        public static RegionEndpoint bucketRegion = RegionEndpoint.USEast1; //default region, change 
        public static string bucketName = "{ s3 bucket name }";
    }

}
