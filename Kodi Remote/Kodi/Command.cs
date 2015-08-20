using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    interface ICommand
    {

        Task<IJsonValue> Fire();

        bool Ok();

        object Result();

    }
}
