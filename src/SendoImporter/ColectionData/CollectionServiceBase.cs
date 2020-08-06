using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.ColectionData
{
    public abstract class CollectionServiceBase<TResult> : ICollectionService<TResult>, ICollectionCommand<TResult>
        where TResult : class, new()
    {
        private TResult _result;
        public DateTime From;
        public DateTime To;

        public CollectionServiceBase()
        {
            _result = (TResult)Activator.CreateInstance(typeof(TResult));
        }

        public TResult Result => _result;

        public void MappingParams(Action<CollectionServiceBase<TResult>> mapping)
        {
            mapping(this);
        }
        public abstract Task<TResult> Collect(string token, DateTime from, DateTime to);
        public abstract Task<string> GetToken();

        public async Task Execute()
        {
            string token = await GetToken();
            _result = await Collect(token, From, To);
        }

    }
}
