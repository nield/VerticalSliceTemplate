namespace VerticalSliceTemplate.Api.Common;

public static class Constants
{
    public static class ApiTags
    {
        public const string Weather = "Weather";
        public const string Todos = "ToDos";
    }
    
    public static class ApiRoutes
    {
        public const string Weather = "/weather";
        public const string Todos = "/todos";
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
