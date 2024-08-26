using Application._Common.Models;

namespace Application._Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}