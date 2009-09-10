namespace UnconstrainedMelody
{
    public class Delegates
    {
        // Just a sample to check the constraint works...
        public static T NoOp<T>(T input) where T : DelegateConstraint
        {
            return input;
        }
    }
}
