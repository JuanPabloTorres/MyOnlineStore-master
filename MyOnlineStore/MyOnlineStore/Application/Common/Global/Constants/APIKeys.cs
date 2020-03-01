using Xamarin.Essentials;

namespace MyOnlineStore.Application.Common.Global.Constants
{
    public static class APIKeys
    {

        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string ServerBackendUrl =
           DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.10.120:44100/api" : "http://192.168.10.120:44100/api";
        public static string LocalBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.132:5000/api" : "http://192.168.1.132:5000/api";
        //public static string LocalBackendUrl =
        //    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.11.6.188:5000/api" : "http://10.11.6.188:5000/api";
    }
}
