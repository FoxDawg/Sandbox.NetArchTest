using System.Text.Json;
using Newtonsoft.Json;

namespace Infrastructure.Service;

internal class SystemJsonSerializer
{
    public SystemJsonSerializer()
    {
        var options = new JsonSerializerOptions();
    }
}

internal class NewtonsoftJsonSerializer
{
    public NewtonsoftJsonSerializer()
    {
        var options = new JsonSerializerSettings();
    }
}