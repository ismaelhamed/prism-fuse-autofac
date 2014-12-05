using System;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace Microsoft.Practices.Prism
{
    using Microsoft.Practices.Prism.Logging;
    using Microsoft.Practices.Prism.Navigation;
    using Microsoft.Practices.Prism.PubSubEvents;

    public abstract class AutofacPrismApplication : PhonePrismApplication
    {
        // Fields
        private readonly bool useDefaultConfiguration;
        private readonly ContainerBuilder internalBuilder;

        /// <summary>
        /// Gets or sets the default <see cref="IContainer"/> for the application.
        /// </summary>
        public IContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates an instance of the PrismApplication class.
        /// </summary>
        /// <param name="runWithDefaultConfiguration">If <see langword="true"/>, registers default services in the container. This is the default behavior.</param>
        protected AutofacPrismApplication(bool runWithDefaultConfiguration = true)
        {
            internalBuilder = new ContainerBuilder();
            useDefaultConfiguration = runWithDefaultConfiguration;

            Run();
        }

        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        protected override sealed void Run()
        {
            Logger = CreateLogger();
            if (Logger == null)
            {
                throw new InvalidOperationException(Properties.Resources.NullLoggerFacadeException);
            }

            SuspensionManager = CreateSuspensionManager();
            if (SuspensionManager == null)
            {
                throw new InvalidOperationException(Properties.Resources.NullSuspensionManagerException);
            }

            Logger.Log(Properties.Resources.InitializingShell, Category.Debug, Priority.Low);
            InitializeFrame();

            Logger.Log(Properties.Resources.ConfiguringIocContainer, Category.Debug, Priority.Low);
            ConfigureContainer(internalBuilder);

            Logger.Log(Properties.Resources.CreatingContainer, Category.Debug, Priority.Low);
            Container = CreateContainer();
            if (Container == null)
            {
                throw new InvalidOperationException(Properties.Resources.NullContainerException);
            }

            Logger.Log(Properties.Resources.ConfiguringServiceLocatorSingleton, Category.Debug, Priority.Low);
            ConfigureServiceLocator();

            Logger.Log(Properties.Resources.BootstrapperSequenceCompleted, Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Creates the <see cref="IContainer"/> that will be used as the default container.
        /// </summary>
        /// <returns>A new instance of <see cref="IContainer"/>.</returns>        
        protected virtual IContainer CreateContainer()
        {
            return internalBuilder.Build();
        }

        /// <summary>
        /// Override to include your own <see cref="Autofac"/> configuration after the framework has finished its configuration, 
        /// but before the container is created.
        /// </summary>
        /// <param name="builder">Autofac configuration builder</param>
        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance(Logger);
            builder.RegisterInstance(SuspensionManager);
            builder.RegisterInstance(new NavigationService(RootFrame)).As<INavigationService>().SingleInstance();

            if (useDefaultConfiguration)
            {
                RegisterTypeIfMissing(builder, typeof(IServiceLocator), typeof(AutofacServiceLocator), true);
                RegisterTypeIfMissing(builder, typeof(IEventAggregator), typeof(EventAggregator), true);
            }

            // Set a factory for the ViewModelLocator to use the default resolution mechanism to construct view models
            ViewModelLocator.SetDefaultViewModelFactory(t => Container.Resolve(t));
        }

        /// <summary>
        /// Configures the LocatorProvider for the <see cref="Microsoft.Practices.ServiceLocation.ServiceLocator" />.
        /// </summary>
        protected override void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
        }

        /// <summary>
        /// Registers the type if missing.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="fromType">From type.</param>
        /// <param name="toType">To type.</param>
        /// <param name="registerAsSingleton">if set to <c>true</c> [register as singleton].</param>
        private void RegisterTypeIfMissing(ContainerBuilder builder, Type fromType, Type toType, bool registerAsSingleton)
        {
            if (fromType == null)
                throw new ArgumentNullException("fromType");

            if (toType == null)
                throw new ArgumentNullException("toType");

            if (registerAsSingleton)
                builder.RegisterType(toType).As(fromType).SingleInstance();
            else
                builder.RegisterType(toType).As(fromType);

        }
    }
}
