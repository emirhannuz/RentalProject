using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelpers
{
    public interface IFileHelper
    {
        string StoragePath { get; set; }
        IDataResult<string> Add(IFormFile file);
        IResult Delete(string path);
    }
}
