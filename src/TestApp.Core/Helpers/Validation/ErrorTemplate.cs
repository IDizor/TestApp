namespace TestApp.Core.Helpers.Validation
{
    /// <summary>
    /// Represents error message templates for Validator.
    /// </summary>
    public static class ErrorTemplate
    {
        public const string Invalid = "Invalid {0}.";
        public const string ShouldHaveValue = "Field {0} should have a value.";
        public const string AlreadyExists = "Specified {0} already exists.";
        public const string DoesNotExists = "Specified {0} does not exist.";
        public const string ShouldBeGreaterThanZero = "Field {0} should be greater than 0.";
    }
}
