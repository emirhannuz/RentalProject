using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
    public class HelperRules<T>
    {
        public static IDataResult<T> Run(params IDataResult<T>[] logics)
        {

            foreach (var logic in logics)
            {
                if (!logic.Success) { return logic; }
            }
            return null;
        }
    }
}
