namespace UnconstrainedMelody
{
    /// <summary>
    /// Provides a set of static utility (and extension) methods for use with delegate
    /// types.
    /// </summary>
    public class Delegates
    {
        /// <summary>
        /// Just a sample to check the constraint works...
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="input">Delegate to return</param>
        /// <returns>The input delegate</returns>
        public static T NoOp<T>(T input) where T : DelegateConstraint
        {
            return input;
        }
    }
}
