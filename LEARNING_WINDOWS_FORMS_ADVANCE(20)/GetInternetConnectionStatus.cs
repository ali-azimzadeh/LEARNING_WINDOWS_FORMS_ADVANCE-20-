using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
{
    public static class GetInternetConnectionStatus
    {
        public static bool IsConnected()
        {
            API.InternetConnectionState flags =
                API.InternetConnectionState.INTERNET_CONNECTION_CONFIGURED;

            bool isConnected = 
                API.InternetGetConnectedState(ref flags, 0);

            return (isConnected);
        }

        public static bool IsInternetConnectionAvailable()
        {
            try
            {
                System.Net.IPHostEntry ipHe =
                           System.Net.Dns.GetHostEntry("www.google.com");

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

    }
 
}
