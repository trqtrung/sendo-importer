using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.ColectionData
{
    public interface ICollectionService<TResult>
        where TResult: class
    {
        Task<string> GetToken();
        Task<TResult> Collect(string token, DateTime from, DateTime to);
    }
}
