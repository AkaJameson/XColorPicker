using XColorPicker;

namespace ColorPickor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            FormManager.Instance.Run();
        }
    }
}