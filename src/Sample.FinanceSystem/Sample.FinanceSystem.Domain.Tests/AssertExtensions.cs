namespace Sample.FinanceSystem.Domain.Tests
{
    public static class AssertExtensions
    {
        public static T Test<T>(this T obj, Action<T> action) where T : class
        {
            action(obj);
            return obj;
        }

        public static T AssertFail<T>(this T obj, string message) where T : class
        {
            Assert.Fail(message);
            return obj;
        }
    }
}
