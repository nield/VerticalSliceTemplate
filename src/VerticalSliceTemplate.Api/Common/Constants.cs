namespace VerticalSliceTemplate.Api.Common;

public static class Constants
{
    public static class OpenApi
    {
        public static class Tags
        {
            public const string Weather = "Weather";
            public const string Todos = "ToDos";
        }
    }

    public static class Headers
    {
        public const string CorrelationId = "x-correlation-id";
        public const string UserProfileId = "UserProfileId";
        public const string Authorization = "Authorization";
    } 

    public static class Environments
    {
        public const string Test = "Test";
    }
}
