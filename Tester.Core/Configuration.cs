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

        serviceCollection.AddScoped<IQuestionService, QuestionService>();
        serviceCollection.AddScoped<ITestService, TestService>();
        serviceCollection.AddScoped<IAnswerOptionService, AnswerOptionService>();
        serviceCollection.AddScoped<IUserAnswerService, UserAnswerService>();
        serviceCollection.AddScoped<ITestResultService, TestResultService>();
        serviceCollection.AddScoped<IUserService, UserService>();
    }
}
