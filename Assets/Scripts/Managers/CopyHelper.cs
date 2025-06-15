using Newtonsoft.Json;

namespace Managers
{
    public static class CopyHelper
    {
        public static T DeepCopy<T>(T original)
        {
            var json = JsonConvert.SerializeObject(original);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}