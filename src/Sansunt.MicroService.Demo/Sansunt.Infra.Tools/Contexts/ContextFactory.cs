using Sansunt.Infra.Tools.Helpers;

namespace Sansunt.Infra.Tools.Contexts {
    /// <summary>
    /// 上下文工厂
    /// </summary>
    public static class ContextFactory {
        /// <summary>
        /// 创建上下文
        /// </summary>
        public static IContext Create() {
            if ( Web.HttpContext == null )
                return NullContext.Instance;
            return new WebContext();
        }
    }
}
