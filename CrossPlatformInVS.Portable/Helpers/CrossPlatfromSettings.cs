using CrossPlatformInVS.Portable.Interfaces;

namespace CrossPlatformInVS.Portable.Helpers
{
    public static class CrossPlatfromSettings
    {
        public static ISettings Instance { get; set; }

        /// <summary>
        /// Gets or sets if we should display items in referse order
        /// </summary>
        public static bool ReverseOrder
        {
            get { return Instance.GetValueOrDefault("ReverseOrder", false); }
            set
            {
                if (Instance.AddOrUpdateValue("ReverseOrder", value))
                    Instance.Save();
            }
        }

    }
}
