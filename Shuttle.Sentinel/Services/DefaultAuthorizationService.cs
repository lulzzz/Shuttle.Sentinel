using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Sentinel
{
    public class DefaultAuthorizationService : IAuthorizationService, IAnonymousPermissions
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ISystemUserQuery _systemUserQuery;
        private readonly ISystemRoleQuery _systemRoleQuery;

        public DefaultAuthorizationService(IDatabaseContextFactory databaseContextFactory, ISystemUserQuery systemUserQuery, ISystemRoleQuery systemRoleQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(systemUserQuery, "systemUserQuery");
            Guard.AgainstNull(systemRoleQuery, "systemRoleQuery");

            _databaseContextFactory = databaseContextFactory;
            _systemUserQuery = systemUserQuery;
            _systemRoleQuery = systemRoleQuery;
        }

        public IEnumerable<string> Permissions(string username, object authenticationTag)
        {
            return new List<string> { "*" };
        }

        public IEnumerable<string> AnonymousPermissions()
        {
            int count;
            var result = new List<string>();

            using (_databaseContextFactory.Create())
            {
                count = _systemUserQuery.Count();
            }

            result.AddRange(_systemRoleQuery.Permissions("Anonymous"));

            if (count == 0)
            {
                result.Add(SystemPermissions.Register.User);
                result.Add(SystemPermissions.Register.InitialAdministrator);
            }

            return result;
        }

        public bool HasPermission(string permission)
        {
            return AnonymousPermissions().Contains(permission);
        }
    }
}