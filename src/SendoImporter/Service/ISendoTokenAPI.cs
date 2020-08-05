using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendoImporter.Service
{
    public interface ISendoTokenAPI
    {
        Task<string> GetToken();
    }
}
