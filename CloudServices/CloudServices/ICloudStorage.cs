using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CloudServices
{
    public interface ICloudStorage
    {
        Task<Stream> Get(string folder, string key);
        Task Put(Stream file, string folder, string fileName);
        Task Delete(string folder, string key);
        Task<IEnumerable<string>> List(string folder);
    }
}
