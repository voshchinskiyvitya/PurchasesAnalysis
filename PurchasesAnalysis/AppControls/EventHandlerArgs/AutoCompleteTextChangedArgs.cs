namespace AppControls.EventHandlerArgs
{
    public class AutoCompleteTextChangedArgs
    {
        public AutoCompleteTextChangedArgs(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
