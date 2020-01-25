using Conexia.Challenge.Domain.Documents;
using FluentNHibernate.Mapping;

namespace Conexia.Challenge.Infra.Data.Mappings
{
    public class DocumentMap : ClassMap<Document>
    {
        public DocumentMap()
        {
            Table("Document");

            Id(x => x.Id)
                .Column("Id")
                .Not.Nullable()
                .GeneratedBy.Native();

            Map(x => x.Name)
                .Column("Name")
                .CustomSqlType("varchar(100)")
                .Length(100)
                .Not.Nullable();

            Map(x => x.Type)
                .Column("Type")
                .CustomType<int>()
                .Not.Nullable();

            Map(x => x.Status)
                .Column("Status")
                .CustomType<int>()
                .Not.Nullable();

            Map(x => x.Situation)
                .Column("Situation")
                .CustomType<int>()
                .Not.Nullable();

            Map(x => x.Created)
                .Column("CreationDate")
                .CustomSqlType("datetime")
                .Not.Nullable();
        }
    }
}
