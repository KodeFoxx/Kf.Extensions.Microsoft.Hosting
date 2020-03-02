![CI @ ubuntu-latest](https://github.com/KodeFoxx/Kf.Extensions.Microsoft.Hosting/workflows/CI%20@%20ubuntu-latest/badge.svg)
![GitHub top language](https://img.shields.io/github/languages/top/kodefoxx/kf.extensions.microsoft.hosting)

# Extensions for IHostBuilder and IHost
`Kf.Extensions.Microsoft.Hosting` are helper classes, structures and extension methods used often in KodeFoxx projects. 
> You can [download/get it on NuGet](https://www.nuget.org/packages/Kf.Extensions.Microsoft.Hosting/).

## Dependencies
- [language-ext](https://github.com/louthy/language-ext)

## Conventions
This library contains some conventions, it assumes the following
- you use the `appsettings.json` file and the `appsettings.{Environment}.json` file to override changes
- serilog, althought it can be overridden using the `configureLoggingDelegate`
- a class as initial startup containing the method `Task Run() { }` is searched for and used as entry point one configured. The constructor of this class can already ask for dependencies from the DI container.

## Example usage
Below is an example of how you can use the builder and add own services.
```
public sealed class Program
    {
        public static void Main(string[] args)
            => ConsoleHostBuilder.CreateAndRunApplication<Program>(
                arguments: args,
                configureServicesDelegate: (hostingContext, services) =>
                {
                    services.AddAndConfigureApplication();
                    services.AddAndConfigureSqlServerPersistence(hostingContext.Configuration);
                });

        public Program(
            ILogger<Program> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        private readonly ILogger<Program> _logger;
        private readonly IMediator _mediator;

        public async Task Run()
        {
            _logger.LogInformation($"Starting application.");

            var amountOfPeople = 0;
            while (amountOfPeople <= 0)
            {
                var people = await GetPeople();
                LogPeople(people);
                amountOfPeople = people.Count();

                if (amountOfPeople == 0)
                    await AddPerson();
            }

            _logger.LogInformation($"Ended application.");
        }

        private async Task<IEnumerable<PersonViewModel>> GetPeople()
            => (await _mediator.Send(new GetPeopleQuery())).IfNullThenEmpty();
        private async Task AddPerson()
            => await _mediator.Send(new AddPersonCommand("Yves", "Schelpe"));
        private void LogPeople(IEnumerable<PersonViewModel> people)
        {
            var amountOfPeople = people.Count();
            var firstTenPeople = String.Join(", ", people.Select(p => $"{p.Number}: '{p.FirstName} {p.LastName}'"));
            if (amountOfPeople == 0)
                _logger.LogWarning("Found #{amountOfPeople} people.", amountOfPeople);
            else
                _logger.LogInformation("Found #{amountOfPeople} people: {people}", amountOfPeople, firstTenPeople);
        }
    }
```