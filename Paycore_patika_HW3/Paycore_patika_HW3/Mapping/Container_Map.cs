using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace Paycore_patika_HW3.Mapping
{
    public class ContainerMap : ClassMapping<Container>
    {
        public ContainerMap()
        {

            Table("container");


            Id(c => c.Id, i =>
            {
                i.Column("id");
                i.Type(NHibernateUtil.Int64);
                i.UnsavedValue(0);
                i.Generator(Generators.Increment);
            });


            Property(c => c.ContainerName, c =>
            {
                c.Column("containername");
                c.Type(NHibernateUtil.String);
                c.Length(50);
                c.NotNullable(false);
            });


            Property(c => c.Latitude, l =>
            {
                l.Column("latitude");
                l.Type(NHibernateUtil.Double);
                l.Precision(10);
                l.Scale(6);
                l.NotNullable(false);
            });


            Property(c => c.Longitude, l =>
            {
                l.Column("longitude");
                l.Type(NHibernateUtil.Double);
                l.Precision(10);
                l.Scale(6);
                l.NotNullable(false);
            });


            Property(c => c.VehicleId, i =>
            {
                i.Column("vehicleid");
                i.Type(NHibernateUtil.Int64);
                i.NotNullable(false);
            });
        }
    }
}
