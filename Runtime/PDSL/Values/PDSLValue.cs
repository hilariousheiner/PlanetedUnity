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

        public int GetIntValue()
        {
            return (int)this.Data;
        }

        public float GetFloatValue()
        {
            return (float)this.Data;
        }
        
        public string GetStringValue()
        {
            return (string)this.Data;
        }

        public Noise GetNoiseValue()
        {
            return (Noise)this.Data;
        }

        public static PDSLValue Null = new PDSLValue(ValueTypeEnum.Null, null);

        public static PDSLValue Integer(int intValue)
        {
            return new PDSLValue(ValueTypeEnum.Int, intValue);
        }

        public static PDSLValue Float(float floatValue)
        {
            return new PDSLValue(ValueTypeEnum.Float, floatValue);
        }

        public static PDSLValue Bool(bool boolValue)
        {
            return new PDSLValue(ValueTypeEnum.Bool, boolValue);
        }

        public static PDSLValue String(string stringValue)
        {
            return new PDSLValue(ValueTypeEnum.String, stringValue);
        }

        public static PDSLValue Tuple(PLTuple tupleValue)
        {
            return new PDSLValue(ValueTypeEnum.Tuple, tupleValue);
        }

        public static PDSLValue List(PLList listValue)
        {
            return new PDSLValue(ValueTypeEnum.List, listValue);
        }

        public static PDSLValue Noise(Noise noise)
        {
            return new PDSLValue(ValueTypeEnum.Noise, noise);
        }
    }
}