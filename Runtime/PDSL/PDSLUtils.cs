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
