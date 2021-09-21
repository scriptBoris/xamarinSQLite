using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinSQLite.Core
{
    public interface IFileSystem
    {
        Task<string> GetAbsDbPath();
    }
}
