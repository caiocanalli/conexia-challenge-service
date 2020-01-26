using Conexia.Challenge.Domain.Users;
using FluentNHibernate.Mapping;

namespace Conexia.Challenge.Infra.Data.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("[User]");

            Id(x => x.Id)
                .Column("Id")
                .Not.Nullable()
                .GeneratedBy.Native();

            Map(x => x.Name)
                .Column("Name")
                .CustomSqlType("varchar(150)")
                .Length(150)
                .Not.Nullable();

            Map(x => x.Email)
                .Column("Email")
                .CustomSqlType("varchar(150)")
                .Length(150)
                .Not.Nullable();

            Map(x => x.Password)
                .Column("Password")
                .CustomSqlType("varchar(150)")
                .Length(150)
                .Not.Nullable();
        }
    }
}
