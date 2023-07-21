using GasNetwork.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GasNetwork.Services
{
    public class ServiceProvider
    {
        private readonly Microsoft.Extensions.DependencyInjection.ServiceProvider? serviceProvider;
        public ServiceProvider()
        {
            var serviceColection = new ServiceCollection();
            ConfigureService(serviceColection);
            serviceProvider = serviceColection.BuildServiceProvider();
        }

        private void ConfigureService(IServiceCollection service)
        {
            service.AddTransient(typeof(MainWindow));
            service.AddTransient(typeof(MainWindowViewModel));
            service.AddSingleton(typeof(TreeNodeViewModel));
            service.AddTransient(typeof(BaseContentViewModel));
            service.AddSingleton(typeof(MeteringUnitViewModel));
            service.AddTransient(typeof(ConsumptionViewModel));
            service.AddTransient(typeof(ArchivesViewModel));
            service.AddTransient(typeof(DataComletenessViewModel));
            service.AddSingleton(typeof(ConsumerViewModel));
            service.AddTransient(typeof(ComplexViewModel));
            service.AddTransient(typeof(MeterViewModel));
            service.AddSingleton(typeof(MeteringDeviceViewModel));
            service.AddSingleton(typeof(DeviceParamsViewModel));
            service.AddTransient(typeof(SurveyViewModel));
            service.AddSingleton(typeof(FlowViewModel));
            service.AddTransient(typeof(ConsumerService));
            service.AddTransient(typeof(DeviceService));
            service.AddSingleton(typeof(SGSTree));
            service.AddSingleton(typeof(SGSConnector));
            service.AddSingleton(typeof(TMRTree));
            service.AddSingleton(typeof(TMRConnector));
            service.AddTransient(typeof(IBuilderTree), typeof(BuilderTreeService));
            service.AddSingleton(typeof(ISettings), typeof(Settings));
            service.AddSingleton(typeof(IDataProvider), typeof(DataProviderService));
            service.AddTransient(typeof(DBConnector));
        }

        public object GetService(Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            return serviceProvider?.GetService(type) ?? throw new NotSupportedException(type.Name);
        }

        public T GetService<T>() where T : class
        {
            return serviceProvider?.GetService<T>() ?? throw new NotSupportedException(typeof(T).Name);
        }
    }
}