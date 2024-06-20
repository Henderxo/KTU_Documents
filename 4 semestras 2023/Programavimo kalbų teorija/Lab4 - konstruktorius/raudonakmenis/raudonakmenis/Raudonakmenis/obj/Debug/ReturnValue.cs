namespace Raudonakmenis.obj.Debug
{
    public class ReturnValue
    {
        private readonly object value;

        public ReturnValue(object value)
        {
            this.value = value;
        }

        public object GetValue()
        {
            return value;
        }
    }
}