#region Copyright
// // -----------------------------------------------------------------------
// // <copyright company="cdmdotnet Limited">
// // 	Copyright cdmdotnet Limited. All rights reserved.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using Cqrs.Ninject.Configuration;
using Ninject;
using MyCqrs1.Domain.Host.Configuration;
using MyCqrs1.Domain.Host.Web.Configuration;

namespace MyCqrs1.Domain.Host.Web
{
    public class WebHost : DomainHost<WebHostModule>
    {
        public IKernel Kernel { get; private set; }

        #region Overrides of DomainHost<WebHostModule>

        protected override DomainConfiguration<WebHostModule> GetDomainConfiguration()
        {
            return new WebDomainConfiguration();
        }

        #endregion


        #region Overrides of DomainHost<WebHostModule>

        public override void Configure()
        {
            Kernel = null;
            NinjectDependencyResolver.DependencyResolverCreator = kernel =>
            {
                Kernel = kernel;
                return new NinjectDependencyResolver(kernel);
            };

            base.Configure();
        }

        protected override void Run()
        {
        }

        #endregion
    }
}