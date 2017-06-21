namespace AppControls.EventHandlerArgs
{
    public class AutoCompleteItemSelectedArgs
    {
        public AutoCompleteItemSelectedArgs(string itemText)
        {
            ItemText = itemText;
        }

        public string ItemText { get; }
    }
}
