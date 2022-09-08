using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace Paycore_patika_HW3.Mapping
{
    public class VehicleMap : ClassMapping<Vehicle>
    {
        public VehicleMap()
        {

            Table("vehicle");


            Id(v => v.Id, i =>
            {
                i.Column("id");
                i.Type(NHibernateUtil.Int64);
                i.UnsavedValue(0);
            });


            Property(v => v.VehicleName, n =>
            {
                n.Column("vehiclename");
                n.Type(NHibernateUtil.String);
                n.Length(50);
                n.NotNullable(false);
            });


            Property(v => v.VehiclePlate, p =>
            {
                p.Column("vehicleplate");
                p.Type(NHibernateUtil.String);
                p.Length(14);
                p.NotNullable(false);
            });

        }
    }
}
