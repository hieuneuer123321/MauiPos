using Android.Net.Http;
using Java.Security;
using Javax.Net.Ssl;
using System.Net.Http;
using Xamarin.Android.Net;

namespace MauiAppUIDemo.Platforms.Android
{
    public class CustomAndroidHttpHandler : AndroidMessageHandler
    {
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            try
            {
                var trustAllCerts = new ITrustManager[] { new IgnoreAllCerts() };
                var sslContext = SSLContext.GetInstance("TLS");
                sslContext.Init(null, trustAllCerts, new SecureRandom());
                return sslContext.SocketFactory;
            }
            catch
            {
                return base.ConfigureCustomSSLSocketFactory(connection);
            }
        }

        private class IgnoreAllCerts : Java.Lang.Object, IX509TrustManager
        {
            public void CheckClientTrusted(Java.Security.Cert.X509Certificate[] chain, string authType) { }
            public void CheckServerTrusted(Java.Security.Cert.X509Certificate[] chain, string authType) { }
            public Java.Security.Cert.X509Certificate[] GetAcceptedIssuers() => new Java.Security.Cert.X509Certificate[0];
        }
    }
}
