namespace Planeted
{
    public class ImportStatement : AStatement
    {
        private string path; 

        public ImportStatement(string path)
        {
            this.path = path;
        }

        public override void Execute(PDSLRuntime runtime)
        {
            PDSL.RunFile(this.path, runtime);
        }
    }
}