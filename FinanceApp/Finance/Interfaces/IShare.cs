using System;
using System.Threading.Tasks;

namespace Finance.Interfaces
{
    public interface IShare
    {
        Task Show(string title, string url);
    }
}
