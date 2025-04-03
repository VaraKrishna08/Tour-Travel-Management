namespace TourTravelApi_Consume
{
    public class CommonVariable
    {
        private static IHttpContextAccessor _HttpContextAccessor;
        static CommonVariable()
        {
            _HttpContextAccessor = new HttpContextAccessor();
        }
        public static int? CustomerID()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("CustomerID") == null)
            {
                return null;
            }
            return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("CustomerID"));
        }
        public static string FullName()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("FullName") == null)
            {
                return null;
            }

            return _HttpContextAccessor.HttpContext.Session.GetString("FullName");
        }
        public static string Email()
        {
            if (_HttpContextAccessor.HttpContext.Session.GetString("Email") == null)
            {
                return null;
            }
            return _HttpContextAccessor.HttpContext.Session.GetString("Email");
        }
    }
}
