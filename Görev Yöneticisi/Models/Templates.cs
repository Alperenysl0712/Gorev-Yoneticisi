namespace Görev_Yöneticisi.Models
{
    public class Templates
    {
        public static Dictionary<int, string> priorityL = new Dictionary<int, string>
            {
                { 1, "Critical" },
                { 2, "Important" },
                { 3, "Routine" }
            };

        public static Dictionary<int, string> status = new Dictionary<int, string>
            {
                { 1, "Active" },
                { 2, "Abandoned" },
                { 3, "Complete" }
            };
    }
}
