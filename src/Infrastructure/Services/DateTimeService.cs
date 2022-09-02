using Blog.Application.Common.Interfaces;

namespace Blog.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
