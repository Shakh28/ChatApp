namespace ChatAppApi.Services
{
    public class MessagesService
    {
        public Dictionary<string, List<Tuple<string, string>>> Messages =
            new Dictionary<string, List<Tuple<string, string>>>()
            {
                {"Dotnet", new List<Tuple<string, string>>()},
                {"Blazor", new List<Tuple<string, string>>()},
                {"Javascript", new List<Tuple<string, string>>()}
    };
    }
}
