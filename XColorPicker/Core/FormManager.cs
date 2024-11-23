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
        private Native native = new Native();
        private PickerForm pickerForm = new PickerForm();
        public IAltEPressProcessor altEPressProcessor;
        private FormManager()
        {
            altEPressProcessor = new ColorPickerCore();
            Application.ApplicationExit += native.Exit;
            Application.ApplicationExit += pickerForm.Exit;
        }

        public void Run()
        {
            Application.Run(pickerForm);
        }
        public void ExitApplication(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
