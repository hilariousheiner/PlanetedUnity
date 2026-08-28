namespace Planeted
{
    public readonly struct PDSLValue
    {
        public ValueTypeEnum Type { get; }
        public object Data { get; }

        private PDSLValue(ValueTypeEnum type, object data)
        {
            this.Type = type; 
            this.Data = data;
        }

        public static PDSLValue Null = new PDSLValue(ValueTypeEnum.Null, null);

        public static PDSLValue Integer(int intValue)
        {
            return new PDSLValue(ValueTypeEnum.Int, intValue);
        }
    }
}