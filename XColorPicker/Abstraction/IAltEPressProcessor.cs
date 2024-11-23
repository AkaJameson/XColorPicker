namespace XColorPicker.Abstraction
{
    public interface IAltEPressProcessor
    {
        /// <summary>
        /// 当 Alt+E 被按下时触发。
        /// </summary>
        void OnAltEPressed();

        /// <summary>
        /// 当用户取消操作（例如重复按下 Alt+E 或右键点击）时触发。
        /// </summary>
        void OnCancel();
    }
}
