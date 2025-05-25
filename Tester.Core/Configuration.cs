using Microsoft.Extensions.DependencyInjection;
using Tester.Core.Services;
using Tester.Core.Services.Interfaces;
using Tester.Data.Repositories.Interfaces;
using Tester.Data.Repositories;

namespace Tester.Core;

public static class Configuration
{
    public static void Configure(IServiceCollection serviceCollection, string connectionString)
    {
        Data.Configuration.Configure(serviceCollection, connectionString);

        serviceCollection.AddAutoMapper(typeof(ModelsMappingProfile));

        serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        serviceCollection.AddTransient<IQuestionService, QuestionService>();
        serviceCollection.AddTransient<ITestService, TestService>();
        serviceCollection.AddTransient<IAnswerOptionService, AnswerOptionService>();
        serviceCollection.AddTransient<IUserAnswerService, UserAnswerService>();
        serviceCollection.AddTransient<ITestResultService, TestResultService>();
    }
}
