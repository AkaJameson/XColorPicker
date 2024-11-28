using ColorPickor;
using System;
using System.Windows.Forms;
using XColorPicker.Abstraction;
using XColorPicker.Core;

namespace XColorPicker
{
    public class FormManager
    {
        private static Lazy<FormManager> instance = new Lazy<FormManager>(() => new FormManager());
        public static FormManager Instance { get { return instance.Value; } }
        private Native native;
        public IAltEPressProcessor altEPressProcessor;
        private FormManager()
        {
            altEPressProcessor = new ColorPickerCore();
        }

        public void Run()
        {
            native = new Native();
            Application.Run(new PickerForm());
        }
        public void ExitApplication(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
