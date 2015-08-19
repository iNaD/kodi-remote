using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    abstract class Command
    {
        abstract public Task<JsonValue> fire();

    }
}
