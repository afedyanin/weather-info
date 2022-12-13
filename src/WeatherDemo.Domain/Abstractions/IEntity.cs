namespace WeatherDemo.Domain.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }

        DateTime Created { get; set; }
    }
}
