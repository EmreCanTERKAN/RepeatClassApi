namespace RepeatClassApi.Types
{
    public class ServiceMessage
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }


    // Buradaki T olan generic yapıya userInfo vericez ve kullanıcı giriş yaptığında ıdsi maili ve typeı dönecek
    public class ServiceMessage<T>
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
