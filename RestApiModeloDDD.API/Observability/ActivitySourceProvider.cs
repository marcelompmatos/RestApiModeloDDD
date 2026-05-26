using System.Diagnostics;

namespace RestApiModeloDDD.API.Observability
{
  
        public static class ActivitySourceProvider
        {
            public static readonly ActivitySource Source =
                new("RestApiModeloDDD.API");
        }
    
}
