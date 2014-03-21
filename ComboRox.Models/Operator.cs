namespace ComboRox.Models
{
    public enum Operator
    {
        /// <summary>
        /// When using this operators for compering object it returns "true" when they are equal
        /// </summary>
        Equals = 1,

        /// <summary>
        /// When using this operators for compering object it returns "true" when they are not equal
        /// </summary>
        NotEquals = 2,

        /// <summary>
        /// When using this operators for compering object it returns "true" when first one is less than second one
        /// </summary>
        LessThan = 3,

        /// <summary>
        /// When using this operators for compering object it returns "true" when first one is greater than second one
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// Use this operator when some object contains another object 
        /// </summary>
        Contains = 5

        // TODO: Add [Less & Greater]ThanOrEquals operator
    }
}