using Drones.Debugging;

namespace Drones
{
    public class DronesConsts
    {
        public const string LocalizationSourceName = "Drones";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "427833aeda3d4987919c06aac1918873";
    }
}
