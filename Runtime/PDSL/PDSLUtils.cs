using System.Collections.Generic;

namespace Planeted
{
    public static class PDSLUtils
    {
        public static void ExpectArgsCount(List<PDSLValue> args, int expected, string functionName)
        {
            if(args.Count != expected)
            {
                throw new PLRuntimeException(functionName + " expects " + expected + " arguments (" + args.Count + " given).", 0);
            }   
        }

        public static int GetIntArg(List<PDSLValue> args, int index, string functionName)
        {
            int result;

            if(!PDSLUtils.TryAsInt(args[index], out result))
            {
                throw new PLRuntimeException(functionName + ": argument " + (index + 1) + " must be an integer.", 0);
            }
            return result;
        }

        public static Noise GetNoiseArg(List<PDSLValue> args, int index, string functionName)
        {
            Noise result = null;

            if (!PDSLUtils.TryAsNoise(args[index], out result))
            {
                throw new PLRuntimeException(functionName + ": argument " + (index + 1) + " must be a noise.", 0);
            }
            return result;
        }

        public static bool TryAsInt(PDSLValue value, out int outVal)
        {
            bool result = false;
            outVal = 0;

            switch(value.Type)
            {
                case ValueTypeEnum.Int:
                    outVal = value.GetIntValue();
                    result = true;
                    break;
                case ValueTypeEnum.Float:
                    outVal = (int)value.GetFloatValue();
                    result = true;
                    break;
                default:
                    break;
            }
            return result;
        }

        public static bool TryAsNoise(PDSLValue value, out Noise outVal)
        {
            bool result = false;
            outVal = null; 

            switch(value.Type)
            {
                case ValueTypeEnum.Noise:
                    outVal = value.GetNoiseValue();
                    result = true;
                    break;
                default:
                    break;
            }    
            return result; 
        }

        public static PDSLValue Negate(PDSLValue value)
        {
            PDSLValue result;

            switch (value.Type)
            {
                case ValueTypeEnum.Float:
                    result = PDSLValue.Float(-((float)value.Data));
                    break;
                case ValueTypeEnum.Int:
                    result = PDSLValue.Integer(-((int)value.Data));
                    break;
                default:
                    throw new PLRuntimeException("cannot negate value.", 0);
            }
            return result;
        }
    }
}