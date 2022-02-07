namespace MauiPlayground
{
    public interface IRepo
    {
        void Test();
    }

    public class Repo : IRepo
    {
        readonly HttpClient _httpClient;
        public Repo(HttpClient c)
        {
            _httpClient = c;
        }

        public void Test()
        {
            //
        }
    }
}
