using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TechnicSolderHelper
{
    public class Debug
    {
        private static readonly string Output =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                "DebugFromModpackHelper.txt");
        private static readonly StringBuilder sb = new StringBuilder();
        private static CheckBox _box;

        public static void AssignCheckbox(CheckBox checkBox)
        {
            _box = checkBox;
        }

        public static void WriteLine(string text, bool forceWriteToFile = false)
        {
            string timestamp = DateTime.Now.ToString(@"[HH:mm:ss]\: ");

            System.Diagnostics.Debug.WriteLine(timestamp + text);

            if (forceWriteToFile || _box != null && _box.Checked)
            {
                sb.AppendLine(timestamp + text);
                Save();
            }
        }

        public static void WriteLine(object o, bool forceWriteToFile = false)
        {
            try
            {
                WriteLine(o.ToString(), forceWriteToFile);
            }
            catch (Exception)
            {
                WriteLine("Error writing object of type " + o.GetType().FullName + " to debug output.");
            }
        }

        public static void Save()
        {
            if (!string.IsNullOrWhiteSpace(sb.ToString()))
                File.AppendAllText(Output, sb.ToString());
            sb.Clear();
        }

        public static void Flush()
        {
            System.Diagnostics.Debug.Flush();
            Save();
        }
    }
}
