using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.ColectionData
{
    public interface ICollectionCommand<TResult>
        where TResult: class
    {
        TResult Result { get; }
        Task Execute();
    }
}
