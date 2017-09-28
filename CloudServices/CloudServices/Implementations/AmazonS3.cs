using Amazon.S3;
using Amazon.S3.Model;
using System.Threading.Tasks;
using System.IO;
using Amazon;
using System;
using CloudServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace CloudServices.Implementations
{
    public class AmazonS3 : ICloudStorage
    {
        private AmazonS3Config _configuration { get; set; }
        private string _accesKey { get; set; }
        private string _secretKey { get; set; }
        private string _bucket { get; set; }
        
        public AmazonS3()
        {
            SetConfiguration();
        }

        public void SetConfiguration()
        {
            _configuration = new AmazonS3Config();
            _configuration.RegionEndpoint = RegionEndpoint.EUWest1;
            _accesKey = "COMPLETE_IT";
            _secretKey = "COMPLETE_IT";
            _bucket = "COMPLETE_IT";
        }

        public async Task<Stream> Get(string folder, string key)
        {
            using (var client = new AmazonS3Client(_accesKey, _secretKey, _configuration))
            {
                var request = new GetObjectRequest
                {
                    BucketName = _bucket,
                    Key = folder + "/" + key
                };
                try
                {
                    using (var response = await client.GetObjectAsync(request))
                    {
                        return response.ResponseStream;
                    }
                }
                catch(Exception exception)
                {
                    throw new CloudStorageException(exception.Message);
                }
            }
        }

        public async Task Put(Stream file, string folder, string key)
        {
            using (var client = new AmazonS3Client(_accesKey, _secretKey, _configuration))
            {
                var request = new PutObjectRequest
                {
                    InputStream = file,
                    BucketName = _bucket,
                    CannedACL = S3CannedACL.PublicRead,
                    Key = folder + "/" + key
                };
                try
                {
                    var response = await client.PutObjectAsync(request);
                }
                catch (Exception exception)
                {
                    throw new CloudStorageException(exception.Message);
                }
            }
        }

        public async Task Delete(string folder, string key)
        {
            using (var client = new AmazonS3Client(_accesKey, _secretKey, _configuration))
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = _bucket,
                    Key = folder + "/" + key
                };
                try
                {
                    var response = await client.DeleteObjectAsync(request);
                }
                catch (Exception exception)
                {
                    throw new CloudStorageException(exception.Message);
                }
            }
        }

        public async Task<IEnumerable<string>> List(string folder)
        {
            using (var client = new AmazonS3Client(_accesKey, _secretKey, _configuration))
            {
                try
                {
                    var keys = new List<string>();
                    var allReaded = false;
                    var marker = "";
                    while(!allReaded)
                    {
                        var request = new ListObjectsRequest { BucketName = _bucket, Prefix = folder, Marker = marker };
                        var response = await client.ListObjectsAsync(request);
                        keys.AddRange(response.S3Objects.Select(o => o.Key));
                        allReaded = !response.IsTruncated;
                    }
                    return keys;
                }
                catch (Exception exception)
                {
                    throw new CloudStorageException(exception.Message);
                }
            }
        }
    }
}
