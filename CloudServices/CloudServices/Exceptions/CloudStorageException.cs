using System;

namespace CloudServices.Exceptions
{
    public class CloudStorageException : Exception
    {
        public CloudStorageException(string message) : base(message)
        {

        }
    }
}
