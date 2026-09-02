using System.Collections.Generic; 

namespace Planeted
{
    public class PDSLProgram
    {
        private List<AStatement> statementList;

        public PDSLProgram(List<AStatement> statmentList)
        {
            this.statementList = statmentList;
        }

        public void Run(PDSLRuntime runtime)
        {
            foreach (AStatement statement in statementList)
            {
                statement.Execute(runtime);
            }
        }
    }
}